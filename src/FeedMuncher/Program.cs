using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeCsv;
using FeedMuncher.IOC.StructureMap;
using ServiceStack.Text;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Track;
using CsvSerializer = ServiceStack.Text.CsvSerializer;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();

			var fullTrackFeed = new TrackFullFeed
			{
				CountryCode = "34"
			};

			var feedDownload = FeedMunch.Download();

			if (feedDownload.FeedAlreadyExists(fullTrackFeed))
			{
				FilterFeedAndWrite(fullTrackFeed);
			}
			else
			{
				var stopwatch = new Stopwatch();
				stopwatch.Start();

				var saveLocally = feedDownload.SaveLocally(fullTrackFeed);
				Console.WriteLine("Downloading to {0}", feedDownload.CurrentFileName);

				var timer = new Timer(x =>
				{
					var fileInfo = new FileInfo(feedDownload.CurrentFileName);
					if (!fileInfo.Exists)
					{
						return;
					}

					var length = fileInfo.Length;
					Console.CursorLeft = 0;
					Console.WriteLine(length);
					Console.CursorTop--;
					Console.CursorVisible = false;

				}, null, 0, 300);

				saveLocally.ContinueWith(task =>
				{
					Console.WriteLine("Finished downloading");
					stopwatch.Stop();
					Console.WriteLine("Took {0} milliseconds to download", stopwatch.ElapsedMilliseconds);
					
					FilterFeedAndWrite(fullTrackFeed);
					timer.Dispose();

				}, TaskContinuationOptions.LongRunning);
			}
			Console.Read();
		}

		private static void FilterFeedAndWrite(Feed feed)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var feedReader = FeedMunch.TrackMunch();
			var rows = feedReader.ReadIntoList(feed);
			
			var filteredFeed = rows.Where(track => track.StreamingReleaseDate < DateTime.Now);

			var feedsFolder = FeedMunch.File().GetOrCreateFeedsFolder();
			var outputFeedPath = Path.Combine(feedsFolder, "output", "testfile.tmp");

			// quicken up by splitting into n arrays
			// parralel write to n files
			// join at the end?
			var timeFilteredFeedWrite = TimerHelper.TimeMe(() =>
			{
				try
				{
					using (var compressedFileStream = File.Create(outputFeedPath))
					{
						using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
						{
							CsvSerializer.SerializeToStream(filteredFeed, compressionStream);
						}
					}
				}
				catch (CsvDeserializationException ex)
				{
					Console.WriteLine(ex.ToString());
				}
			});

			var completedFilePath = outputFeedPath.Replace(".tmp", ".gz");
			TryDelete(completedFilePath);
			File.Move(outputFeedPath, completedFilePath);

			Console.WriteLine("Took {0} milliseconds to output filtered feed", timeFilteredFeedWrite.ElapsedMilliseconds);

			using (var fileStream = File.OpenRead(completedFilePath))
			{
				using (var sr = new StreamReader(fileStream))
				{
					var readLines = sr.ReadLines();
					Console.WriteLine("There are now {0} lines", readLines);
				}
			}
		}

		private static void TryDelete(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}

	public static class TimerHelper
	{
		public static Stopwatch TimeMe(Action toTime)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			toTime();
			stopwatch.Stop();
			return stopwatch;
		}
	}
}
