using System;

namespace DeCsv.Unit.Tests
{
	public class QueryRow
	{
		public string Country { get; set; }
		public string Query { get; set; }
		public string Artist { get; set; }
		public string Title { get; set; }
		public bool Ignore { get; set; }
		public DateTime Date { get; set; }
		public decimal Price { get; set; }
	}
}