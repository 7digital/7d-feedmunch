using System;

namespace DeCsv
{
	public static class TryConvert
	{
		public static int ToInt32<T>(T value)
		{
			return TryTo(() => Convert.ToInt32(value), 0);
		}

		public static short ToInt16<T>(T value)
		{
			return TryTo(() => Convert.ToInt16(value), (short)0);
		}

		public static long ToInt64<T>(T value)
		{
			return TryTo(() => Convert.ToInt64(value), 0);
		}

		public static decimal ToDecimal<T>(T value)
		{
			return TryTo(() => Convert.ToDecimal(value), 0);
		}

		public static float ToSingle<T>(T value)
		{
			return TryTo(() => Convert.ToSingle(value), 0);
		}

		public static double ToDouble<T>(T value)
		{
			return TryTo(() => Convert.ToDouble(value), 0);
		}

		public static DateTime ToDateTime<T>(T value)
		{
			return TryTo(() => Convert.ToDateTime(value), DateTime.MinValue);
		}

		private static TOut TryTo<TOut>(Func<TOut> to, TOut defaultValue)
		{
			try
			{
				return to();
			}
			catch (FormatException)
			{
				return defaultValue;
			}
		}
	}
}