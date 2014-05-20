using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace SevenDigital.FeedMunch
{
	public static class FilterExtensions
	{
		public static IEnumerable<T> Filtrate<T>(this Filter filter, IEnumerable<T> rows)
		{
			return rows.Where(filter.ApplyToRow);
		}

		public static List<T> FiltrateToList<T>(this Filter filter, IEnumerable<T> rows)
		{
			return Filtrate(filter, rows).ToList();
		}

		public static bool ShouldPass(this Filter filter, string value)
		{
			return filter.Operator == FilterOperator.Equals
					? filter.Values.Any(x => x == value)
					: filter.Values.All(x => x != value);
		}

		public static void ApplyToStream(this Filter filter, Stream inputStream, Stream outputStream)
		{
			using (var sr = new StreamReader(inputStream))
			{
				var csvReader = new CsvReader(sr);
				csvReader.Read();

				var headers = csvReader.FieldHeaders;
				var filterFieldIndex = Array.FindIndex(headers, x => x == filter.FieldName);

				if (!string.IsNullOrEmpty(filter.FieldName) && filterFieldIndex < 0)
				{
					throw new ArgumentException(String.Format("Chosen filter field is not valid: \"{0}\", remember field names are case sensitive", filter.FieldName));
				}

				ServiceStack.Text.CsvSerializer.SerializeToStream(headers, outputStream);
				do
				{
					var currentRecord = csvReader.CurrentRecord;
					if ((filterFieldIndex < 0 || filter.ShouldPass(currentRecord[filterFieldIndex])) && !csvReader.IsRecordEmpty())
					{
						ServiceStack.Text.CsvSerializer.SerializeToStream(currentRecord, outputStream);
					}
				} while (csvReader.Read());
			}
		}
	}
}