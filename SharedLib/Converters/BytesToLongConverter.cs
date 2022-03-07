using System;

namespace SharedLib.Converters
{
	public static class BytesToLongConverter
	{
		public static long Convert(this byte[] bytes)
		{
			return System.Convert.ToInt64(HexFromByteArray(bytes), 16);
		}

		private static string HexFromByteArray(byte[] bytes)
		{
			string hex = "0x" + BitConverter.ToString(bytes).Replace("-", string.Empty);
			return hex;
		}
	}
}
