using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Util
{
	internal static class ConsoleUtility
	{
		public static void WritePermutation(byte[] permutation)
		{
			if (permutation == null) throw new ArgumentNullException(nameof(permutation));
			if (permutation.Length != 256) throw new ArgumentException("Permutation must be of appropriate size", nameof(permutation));

			DrawPemutationArray(permutation);
		}

		public static void WritePermutation(byte[] permutation, int seed)
		{
			if (permutation == null) throw new ArgumentNullException(nameof(permutation));
			if (permutation.Length != 255) throw new ArgumentException("Permutation must be of appropriate size", nameof(permutation));

			Console.WriteLine($"Generation seed: {seed}");

			DrawPemutationArray(permutation);
		}

		private static void DrawPemutationArray(byte[] permutation)
		{
			Console.WriteLine();

			for (int i = 0; i < 256; i += 16)
			{
				for (int k = 0; k < 16; k++)
				{
					Console.Write("  {0,3}", permutation[i + k]);
				}
				Console.WriteLine();
			}

			Console.WriteLine();
		}
	}
}
