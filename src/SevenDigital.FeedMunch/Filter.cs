using System;
using System.Collections.Generic;

namespace SevenDigital.FeedMunch
{
	public class Filter
	{
		private const char VALUE_DELIMETER = ',';

		public string FieldName { get; private set; }
		public FilterOperator Operator { get; private set; }
		public IEnumerable<string> Values { get; private set; }

		public Filter(string rawFilter)
		{
			Parse(rawFilter);
		}

		private void Parse(string rawFilter)
		{
			var strings = rawFilter.Split(new[] {"!="}, StringSplitOptions.RemoveEmptyEntries);
			if (strings.Length == 1)
			{
				strings = rawFilter.Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);
				Operator = FilterOperator.Equals;
			}

			if (strings.Length != 2)
			{
				throw new ArgumentException("Could not parse filter, should be in the format {fieldName}[=]|[!=]{array of values} e.g. licensorId=1,2,3 ");
			}

			FieldName = strings[0];
			Values = strings[1].Split(VALUE_DELIMETER);
		}
	}
}