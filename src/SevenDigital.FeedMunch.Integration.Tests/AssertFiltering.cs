using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using DeCsv;
using NUnit.Framework;

namespace SevenDigital.FeedMunch.Integration.Tests
{
	public class AssertFiltering
	{
		public static void IsAsExpected<T>(string outputFile, Func<T, bool> filteringPredicate)
		{
			using (var fileStream = File.OpenRead(outputFile))
			{
				using (var gZipStream = new GZipStream(fileStream, CompressionMode.Decompress))
				{
					var deSerialize = CsvDeserialize.DeSerialize<T>(gZipStream).ToList();
					Assert.That(deSerialize.Any(filteringPredicate), Is.True, "No items matching filtering predicate are found");
					Assert.That(deSerialize.All(filteringPredicate), Is.True, "Not all items match filtering predicate");
				}
			}
		}
	}
}