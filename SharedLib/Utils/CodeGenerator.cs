using System;
using System.Linq;

namespace SharedLib.Utils
{
	public static class CodeGenerator
	{
		private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		private static Random random = new Random();

		public static string GetRandomString(int length)
		{

			return new string(Enumerable.Repeat(Chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static string GetRandomNumberString(int length)
		{
			//TODO: удалить
			return "1234";
			//return new string(Enumerable.Repeat(Numbers, length)
			//	.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
