using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DeCsv
{
	public class CsvDeserializationException : SerializationException
	{
		public CsvDeserializationException(string message)
			: base(message)
		{ }

		public string RowRaw { get; set; }
		public IEnumerable<string> RowFields { get; set; }
		public IEnumerable<string> HeaderFields { get; set; }

		public override string ToString()
		{
			return string.Format("{0} Rows: {1} Headers:{2}", base.Message, RowRaw, string.Join(",", HeaderFields));
		}
	}
}