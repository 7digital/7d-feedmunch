using System;
using System.Reflection;

namespace DeCsv
{
	public static class PropertyConvertor
	{
		public static void SetValue<T>(object item, PropertyInfo property, T value)
		{
			var methodInfo = property.GetSetMethod();

			if (property.PropertyType == typeof(Boolean))
			{
				methodInfo.Invoke(item, new object[] { Convert.ToBoolean(value) });
			}
			else if (property.PropertyType == typeof(int))
			{
				methodInfo.Invoke(item, new object[] { TryConvert.ToInt32(value) });
			}
			else if (property.PropertyType == typeof(short))
			{
				methodInfo.Invoke(item, new object[] { TryConvert.ToInt16(value) });
			}
			else if (property.PropertyType == typeof(long))
			{
				methodInfo.Invoke(item, new object[] { TryConvert.ToInt64(value) });
			}
			else if (property.PropertyType == typeof(decimal))
			{
				methodInfo.Invoke(item, new object[] { TryConvert.ToDecimal(value) });
			}
			else if (property.PropertyType == typeof(float))
			{
				methodInfo.Invoke(item, new object[] { TryConvert.ToSingle(value) });
			}
			else if (property.PropertyType == typeof(double))
			{
				methodInfo.Invoke(item, new object[] { TryConvert.ToDouble(value) });
			}
			else if (property.PropertyType == typeof(DateTime))
			{
				methodInfo.Invoke(item, new object[] { TryConvert.ToDateTime(value) });
			}
			else
			{
				methodInfo.Invoke(item, new object[] { value });
			}
		}

		public static PropertyInfo GetProperty<T>(string propertyName)
		{
			var type = typeof(T);
			var propertyInfo = type.GetProperty(propertyName)
			                   ?? type.GetProperty(propertyName.UppercaseFirst());

			if (propertyInfo == null)
				throw new CsvDeserializationException(String.Format("PropertyName \"{0}\" is not a property of type {1}", propertyName, type));

			return propertyInfo;
		}
	}
}