using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hex.VM.Core.Helper
{
	public class Generator
	{
		public static string RandomAlphabet()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
			return new string(Enumerable.Repeat(chars, new Random(Guid.NewGuid().GetHashCode()).Next(80) + 10)
				.Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
		}

		public static int NextInt(int min, int max)
		{
			return new Random(Guid.NewGuid().GetHashCode()).Next(min, max+1);
		}
	}
}