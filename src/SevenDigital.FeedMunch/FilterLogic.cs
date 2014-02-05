using System.Linq;

namespace SevenDigital.FeedMunch
{
	public interface IFilterLogic
	{
		bool ShouldPass(string value, Filter filter);
	}

	public class FilterLogic : IFilterLogic
	{
		public bool ShouldPass(string value, Filter filter)
		{
			return filter.Operator == FilterOperator.Equals
				       ? filter.Values.Any(x => x == value)
				       : filter.Values.All(x => x != value);
		}
	}
}