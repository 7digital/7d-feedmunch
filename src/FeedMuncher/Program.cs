using System;
using FeedMuncher.IOC.StructureMap;
using SevenDigital.Api.FeedReader.Feeds.Track;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();
			
			var fullTrackFeed = new TrackFullFeed()
			{
				CountryCode = "34"
			};

			var feedDownload = FeedMunch.Fluent();

			feedDownload.DoTheWholeThing(fullTrackFeed); // DO IT - THE WHOLE THING!! (This will be split into a fluent api, jsut had to move it out of here, was hurting my head!)

			Console.Read();
		}
	}
}
