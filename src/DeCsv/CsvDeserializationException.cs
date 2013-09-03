using System.Runtime.Serialization;

namespace DeCsv
{
	public class CsvDeserializationException : SerializationException
	{
		public CsvDeserializationException(string message)
			: base(message)
		{ }
	}
}