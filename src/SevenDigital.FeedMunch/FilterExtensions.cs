using System.Collections.Generic;
using System.Linq;

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
	}
}