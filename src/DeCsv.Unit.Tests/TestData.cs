using System;

namespace DeCsv.Unit.Tests
{
	public static class TestData
	{
		public static string TestCsv = "country,query,artist,title,ignore,date,price" + Environment.NewLine +
									   "US,Your Song,Elton John,Your Song,false,2008-03-01T00:00:00Z,0.99" + Environment.NewLine +
									   "US,Patience guns n roses,Guns 'n Roses,Patience,false,2008-03-01T00:00:00Z,0.99";

		public static string TestCsvBlankColumn = "country,query,artist,title,ignore,date,price" + Environment.NewLine +
												  "US,,Guns 'n Roses,Patience,false,2008-03-01T00:00:00Z,0.99";

		public static string TestCsvCommaColumn = "country,query,artist,title,ignore,date,price" + Environment.NewLine +
												  "UK,\"Definately, Maybe\",Oasis,\"Definately, Maybe\",false,2008-03-01T00:00:00Z,0.99";

		public static string TestCsvNotMatching = "country,query,artist,title,ignore,date,price" + Environment.NewLine +
												  "UK,Definately, Maybe,Oasis,Definately, Maybe,false,2008-03-01T00:00:00Z,0.99";

		public static string TestCsvRowLessThanHeader = "country,query,artist,title,ignore,date,price" + Environment.NewLine +
												  "UK,Definately Maybe,Oasis,Definately Maybe,false,2008-03-01T00:00:00Z";

		public static string TestCsvBlankPrice = "country,query,artist,title,ignore,date,price" + Environment.NewLine +
												  "US,,Guns 'n Roses,Patience,false,2008-03-01T00:00:00Z,";

		public static string TestCsvMissingFinalComma = "country,query,artist,title,ignore,date,price" + Environment.NewLine +
									   "US,Your Song,Elton John,Your Song,false,2008-03-01T00:00:00Z,0.99" + Environment.NewLine +
									   "US,Patience guns n roses,Guns 'n Roses,Patience,false,2008-03-01T00:00:00Z";
	}
}
