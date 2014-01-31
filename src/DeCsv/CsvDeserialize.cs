using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DeCsv
{
	public static class CsvSerializer
	{
		public static IEnumerable<T> DeserializeEnumerableFromStream<T>(Stream stream)
		{
			return CsvDeserialize.DeSerialize<T>(stream);
		} 
	}

	public static class CsvDeserialize
	{
		private const char DELIMETER = ',';

		/// <exception cref="CsvDeserializationException"></exception>
		public static IEnumerable<TEntity> DeSerialize<TEntity>(string newlineRowDelimetedCsv)
		{
			var rows = newlineRowDelimetedCsv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			return DeSerialize<TEntity>(rows);
		}

		/// <exception cref="CsvDeserializationException"></exception>
		public static IEnumerable<TEntity> DeSerialize<TEntity>(string[] rows)
		{
			const int toSkip = 1;

			var headings = rows.ElementAt(0).Split(new[] { DELIMETER }, StringSplitOptions.None);
			var values = rows.Skip(toSkip);

			var properties = headings.Select(PropertyConvertor.GetProperty<TEntity>).ToArray();

			return values.Select(row => BuildEntity<TEntity>(properties, row));
		}

		/// <exception cref="CsvDeserializationException"></exception>
		public static IEnumerable<TEntity> DeSerialize<TEntity>(Stream stream)
		{
			using (var streamReader = new StreamReader(stream, Encoding.Default))
			{
				var properties = new PropertyInfo[] { };
				string row;
				var isFirstRow = true;
				while ((row = streamReader.ReadLine()) != null)
				{
					if (isFirstRow)
					{
						var headings = row.Split(new[] { DELIMETER }, StringSplitOptions.None);
						properties = headings.Select(PropertyConvertor.GetProperty<TEntity>).ToArray();
						isFirstRow = false;
					}
					else
					{
						yield return BuildEntity<TEntity>(properties, row);
					}
				}
			}
		}

		private static TEntity BuildEntity<TEntity>(IList<PropertyInfo> entitySchema, string row)
		{
			var entity = Activator.CreateInstance<TEntity>();

			var strings = row.SplitCsvRowHandlingQuotes(DELIMETER).ToList();

			if (strings.Count > entitySchema.Count)
			{
				throw new CsvDeserializationException("Row length is greater than header row length")
				{
					HeaderFields = entitySchema.Select(x=>x.Name),
					RowFields = strings,
					RowRaw = row
				};
			}

			if (strings.Count < entitySchema.Count)
			{
				var shouldBe = entitySchema.Count;
				var actual = strings.Count;
				var diff = shouldBe - actual;
				for (var count = 0; count < diff; count++)
				{
					strings.Add("");
				}
			}
			For(0, strings.Count, i => PropertyConvertor.SetValue(entity, entitySchema[i], strings[i]));

			return entity;
		}

		private static void For(int fromInclusive, int toExclusive, Action<int> loopAction)
		{
			for (var i = fromInclusive; i < toExclusive; i++)
			{
				loopAction(i);
			}
		}
	}
}
