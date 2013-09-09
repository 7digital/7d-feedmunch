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

		public Stream GetDecompressedStream(Feed feed)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(BuildFullFilepath(feed), FileMode.Open, FileAccess.Read);
				return new GZipStream(fileStream, CompressionMode.Decompress);
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				throw;
			}
		}

		private string BuildFullFilepath(Feed suppliedFeed)
		{
			return Path.Combine(_fileHelper.GetOrCreateFeedsFolder(), suppliedFeed.GetLatest());
		}
	}
}