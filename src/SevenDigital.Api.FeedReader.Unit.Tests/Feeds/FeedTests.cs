namespace SevenDigital.Api.FeedReader.Unit.Tests.Feeds
{
	public static class TestFeed
	{
		public static string BasicTrackFeed()
		{
			return "trackId,title,version,type,isrc,explicitContent,trackNumber,discNumber,artistId,artistAppearsAs,releaseId,duration,formats,price,rrp,url,popularity,streamingReleaseDate " +
			"1660,Snowed Under,,Audio,GBAAN0300721,false,2,1,1,Keane,135,228,\"17,55,56,26\",1.69,1.69,http://www.zdigital.com.au/artist/keane/release/somewhere-only-we-know-enhanced?h=02,0.36,2004-05-17T00:00:00Z" +
			"1661,Walnut Tree,,Audio,GBAAN0300720,false,3,1,1,Keane,135,220,\"17,55,56,26\",1.69,1.69,http://www.zdigital.com.au/artist/keane/release/somewhere-only-we-know-enhanced?h=03,0.35,2004-05-17T00:00:00Z";
		}

		public static string BasicArtistFeed()
		{
			return "";
		}

		public static string BasicReleaseFeed()
		{
			return "";
		}
	}
}
