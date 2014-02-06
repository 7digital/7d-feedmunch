using System.IO;
using System.IO.Compression;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public class FeedUnpacker : IFeedUnpacker
	{
		private readonly IFileHelper _fileHelper;

		public FeedUnpacker(IFileHelper fileHelper)
		{
			_fileHelper = fileHelper;
		}

		public FileStream GetFeedAsFilestream(Feed feed)
		{
			return new FileStream(BuildFullFilepath(feed), FileMode.Open, FileAccess.Read);
		}

		public GZipStream GetDecompressedStream(Stream stream, Feed feed)
		{
			try
			{
				return new GZipStream(stream, CompressionMode.Decompress);
			}
			catch
			{
				stream.Close();
				throw;
			}
		}

		private string BuildFullFilepath(Feed suppliedFeed)
		{
			//if (!string.IsNullOrEmpty(suppliedFeed.ExistingPath))
			//{
				return suppliedFeed.ExistingPath;
			//}

			//return Path.Combine(_fileHelper.GetOrCreateFeedsFolder(), suppliedFeed.GetLatest());
		}
	}
}