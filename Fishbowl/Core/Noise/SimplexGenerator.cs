using Fishbowl.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Noise
{

	///<summary>Noise generator using the Simplex Perlin math.</summary>
	/// <remarks>
	/// Something about Simplex Generation implementation is just universally cursed.
	/// It produces triangular artifacts in the noise, suggesting there's an issue identifying
	/// the current simplex (or the point along the simplex gradient).
	/// 
	/// However of the six implementations found and read online not one of them
	/// doesn't produce artifacts. (Some of these are from universities). Even more troubling
	/// is that all of these implementations (universities included) are refactors of the
	/// same buggy source code.
	/// 
	/// Some of the issue is that there are order of operations errors in the source code that
	/// aren't fixed in any of the reproductions.
	/// 
	/// <see cref="PerlinGenerator"/> is recommended for the time being.
	/// 
	/// A custom noise generator may be necessary in the future.
	/// </remarks>
	internal class SimplexGenerator : IOctaveNoise
	{
		#region Static
		private static byte[] GeneratePermutation()
		{
			byte[] data = new byte[256];

			LinkedList<byte> scrambleList = new();

			for (int i = 255; i >= 0; i--)
			{ scrambleList.AddFirst((byte)i); }

			int ptr = 256;
			while (ptr --> 0)
			{
				data[ptr] = scrambleList.PopIndex(Simulation.Random.GetInt(scrambleList.Count));
			}

			return data;
		}

		private static byte[] GeneratePermutation(int generatorId)
		{
			byte[] data = new byte[256];

			LinkedList<byte> scrambleList = new();

			for (int i = 255; i >= 0; i--)
			{ scrambleList.AddFirst((byte)i); }

			int ptr = 256;
			while (ptr --> 0)
			{
				data[ptr] = scrambleList.PopIndex(Simulation.Random.CustomGetInt(generatorId, scrambleList.Count));
			}

			return data;
		}

		public static readonly ReadOnlyArray<byte> ReferencePermutation = ReadOnlyArray<byte>.WrapArray(new byte[256]
		{
			151, 160, 137,  91,  90,  15, 131,  13, 201,  95,  96,  53, 194, 233,   7, 225,
			140,  36, 103,  30,  69, 142,   8,  99,  37, 240,  21,  10,  23, 190,   6, 148,
			247, 120, 234,  75,   0,  26, 197,  62,  94, 252, 219, 203, 117,  35,  11,  32,
			 57, 177,  33,  88, 237, 149,  56,  87, 174,  20, 125, 136, 171, 168,  68, 175,
			 74, 165,  71, 134, 139,  48,  27, 166,  77, 146, 158, 231,  83, 111, 229, 122,
			 60, 211, 133, 230, 220, 105,  92,  41,  55,  46, 245,  40, 244, 102, 143,  54,
			 65,  25,  63, 161,   1, 216,  80,  73, 209,  76, 132, 187, 208,  89,  18, 169,
			200, 196, 135, 130, 116, 188, 159,  86, 164, 100, 109, 198, 173, 186,   3,  64,
			 52, 217, 226, 250, 124, 123,   5, 202,  38, 147, 118, 126, 255,  82,  85, 212,
			207, 206,  59, 227,  47,  16,  58,  17, 182, 189,  28,  42, 223, 183, 170, 213,
			119, 248, 152,   2,  44, 154, 163,  70, 221, 153, 101, 155, 167,  43, 172,   9,
			129,  22,  39, 253,  19,  98, 108, 110,  79, 113, 224, 232, 178, 185, 112, 104,
			218, 246,  97, 228, 251,  34, 242, 193, 238, 210, 144,  12, 191, 179, 162, 241,
			 81,  51, 145, 235, 249,  14, 239, 107,  49, 192, 214,  31, 181, 199, 106, 157,
			184,  84, 204, 176, 115, 121,  50,  45, 127,   4, 150, 254, 138, 236, 205,  93,
			222, 114,  67,  29,  24,  72, 243, 141, 128, 195,  78,  66, 215,  61, 156, 180,
    });

		private static float Gradient(int hash, float x)
		{
			float grad = 1.0f + (hash & 0x07);
			if ((hash & 8) != 0) grad = -grad;
			return grad * x;
		}

		private static float Gradient(int hash, float x, float y)
		{
			float u = (hash & 1) > 0 ? x : y;
			float v = (hash & 1) > 0 ? y : x;
			return ((hash & 2) > 0 ? -u : u) + ((hash & 4) > 0 ? -2.0f * v : 2.0f * v);
		}

		private static float Gradient(int hash, float x, float y, float z)
		{
			byte h = (byte)(hash & 0x1F);
			float u, v;

			if (h < 11) // 11/32 chance
			{
				u = x;
				v = y;
			}
			else if (h < 22) // 11/32 chance
			{
				u = x;
				v = z;
			}
			else //10/32 chance [biased against!]
			{
				u = y;
				v = z;
			}

			return ((h & 32) > 0 ? -u : u) + ((h & 64) > 0 ? -v : v);
		}

		private static int CustomFloor(float fp)
		{
			int i = (int)fp;
			return (fp < i) ? (i - 1) : i;
		}

		#endregion

		const float REFERENCE_FREQUENCY = 1.0f;
		const float REFERENCE_AMPLITUDE = 1.0f;
		const float REFERENCE_LACUNARITY = 2.0f;
		const float REFERENCE_PERSISTENCE = 0.5f;

		private float Frequency;
		private float Amplitude;
		private float Lacunarity;
		private float Persistence;
		private ReadOnlyArray<byte> Permutation { get; init; }
		private SimplexGenerator() { Permutation = Array.Empty<byte>(); }



		public ReadOnlyArray<byte> GetPermutation() => Permutation;

		public float Generate(float x)
		{
			float n0, n1;

			int
				i0 = CustomFloor(x),
				i1 = i0 + 1;

			float
				x0 = x - 10,
				x1 = x0 - 1.0f;

			float t0 = 1.0f - (x0 * x0);
			t0 *= t0;
			n0 = t0 * t0 * Gradient(Permutation[i0], x0);

			float t1 = 1.0f - (x1 * x1);
			t1 *= t1;
			n1 = t1 * t1 * Gradient(Permutation[i1], x1);

			// Scales to [-1, 1]
			// Need source for magic number.
			// return 0.395f * (n0 + n1);

			// Scales to [0, 1]
			return ((0.395f * (n0 + n1)) + 1.0f) / 2.0f;
		}

		public float Generate(float x, float y)
		{
			const float F2 = 0.366025403f;  // F2 = (sqrt(3) - 1) / 2
			const float G2 = 0.211324865f;  // G2 = (3 - sqrt(3)) / 6   = F2 / (1 + 2 * K)

			float
				n0,
				n1,
				n2,
				skew = (x + y) * F2,
				xs = x + skew,
				ys = y + skew;

			int
				i = CustomFloor(xs),
				k = CustomFloor(ys),
				i1,
				k1;

			float
				t = G2 * (i + k),
				X0 = i - t,
				Y0 = k - t,
				x0 = x - X0,
				y0 = y - Y0;

			if (x0 > y0)
			{
				i1 = 1;
				k1 = 0;
			}
			else
			{
				i1 = 0;
				k1 = 1;
			}

			float
				x1 = x0 - i1 + G2,
				y1 = y0 - k1 + G2,
				x2 = x0 - 1.0f + (2.0f * G2),
				y2 = y0 - 1.0f + (2.0f * G2);

			int
				gi0 = Permutation[(byte)(i + Permutation[(byte)k])],
				gi1 = Permutation[(byte)(i + i1 + Permutation[(byte)(k + k1)])],
				gi2 = Permutation[(byte)(i + 1 + Permutation[(byte)(k + 1)])];

			// First corner.
			float t0 = 0.5f - (x0 * x0) - (y0 * y0);
			if (t0 < 0.0f)
				n0 = 0.0f;
			else
			{
				t0 *= t0;
				n0 = t0 * t0 * Gradient(gi0, x0, y0);
			}

			// Second corner.
			float t1 = 0.5f - (x1 * x1) - (y1 * y1);
			if (t1 < 0.0f)
				n1 = 0.0f;
			else
			{
				t1 *= t1;
				n1 = t1 * t1 * Gradient(gi1, x1, y1);
			}

			// Third corner.
			float t2 = 0.5f - (x2 * x2) - (y2 * y2);
			if (t2 < 0.0f)
				n2 = 0.0f;
			else
			{
				t2 *= t2;
				n2 = t2 * t2 * Gradient(gi2, x2, y2);
			}

			// Scales to [-1, 1]
			// Source of magic number needed.
			// return 45.23065f * (n0 + n1 + n2);

			// Scales to [0, 1]
			return ((70f * (n0 + n1 + n2)) + 1.0f) / 2.0f;
		}

		public float Generate(float x, float y, float z)
		{
			const float F3 = 1.0f / 3.0f;
			const float G3 = 1.0f / 6.0f;

			float n0, n1, n2, n3,
				skew = (x + y + z) * F3;

			int
				i = CustomFloor(x + skew),
				j = CustomFloor(y + skew),
				k = CustomFloor(z + skew);

			float
				t = (i + j + k) * G3,
				X0 = i - t,
				Y0 = j - t,
				Z0 = k - t,
				x0 = x - X0,
				y0 = y - Y0,
				z0 = z - Z0;

			int
				i1,
				j1,
				k1,
				i2,
				j2,
				k2;

			if (x0 >= y0)
			{
				if (y0 >= z0)
				{
					i1 = 1; // X Y Z order
					j1 = 0;
					k1 = 0;
					i2 = 1;
					j2 = 1;
					k2 = 0;
				}
				else if (x0 >= z0)
				{
					i1 = 1; // X Z Y order
					j1 = 0;
					k1 = 0;
					i2 = 1;
					j2 = 0;
					k2 = 1;
				}
				else
				{
					i1 = 0; // Z X Y order
					j1 = 0;
					k1 = 1;
					i2 = 1;
					j2 = 0;
					k2 = 1;
				}
			}
			else // x0 < t0
			{
				if (y0 < z0)
				{
					i1 = 0; // Z Y X order
					j1 = 0;
					k1 = 1;
					i2 = 0;
					j2 = 1;
					k2 = 1;
				}
				else if (x0 < z0)
				{
					i1 = 0; // Y Z X order
					j1 = 1;
					k1 = 0;
					i2 = 0;
					j2 = 1;
					k2 = 1;
				}
				else
				{
					i1 = 0; // Y X Z order
					j1 = 1;
					k1 = 0;
					i2 = 1;
					j2 = 1;
					k2 = 0;
				}
			}

			float
				x1 = x0 - i1 + G3,
				y1 = y0 - j1 + G3,
				z1 = z0 - k1 + G3,
				x2 = x0 - i2 + (2.0f * G3),
				y2 = y0 - j2 + (2.0f * G3),
				z2 = z0 - k2 + (2.0f * G3),
				x3 = x0 - 1.0f + (3.0f * G3),
				y3 = y0 - 1.0f + (3.0f * G3),
				z3 = z0 - 1.0f + (3.0f * G3);

			int
				gi0 = Permutation[(byte)(i + Permutation[(byte)(j + Permutation[(byte)k])])],
				gi1 = Permutation[(byte)(i + i1 + Permutation[(byte)(j + j2 + Permutation[(byte)(k + k1)])])],
				gi2 = Permutation[(byte)(i + i2 + Permutation[(byte)(j + j2 + Permutation[(byte)(k + k2)])])],
				gi3 = Permutation[(byte)(i + 1 + Permutation[(byte)(j + 1 + Permutation[(byte)(k + 1)])])];

			float t0 = 0.6f - (x0 * x0) - (y0 * y0) - (z0 * z0);
			if (t0 < 0)
			{
				n0 = 0.0f;
			}
			else
			{
				t0 *= t0;
				n0 = t0 * t0 * Gradient(gi0, x0, y0, z0);
			}
			float t1 = 0.6f - (x1 * x1) - (y1 * y1) - (z1 * z1);
			if (t1 < 0)
			{
				n1 = 0.0f;
			}
			else
			{
				t1 *= t1;
				n1 = t1 * t1 * Gradient(gi1, x1, y1, z1);
			}
			float t2 = 0.6f - (x2 * x2) - (y2 * y2) - (z2 * z2);
			if (t2 < 0)
			{
				n2 = 0.0f;
			}
			else
			{
				t2 *= t2;
				n2 = t2 * t2 * Gradient(gi2, x2, y2, z2);
			}
			float t3 = 0.6f - (x3 * x3) - (y3 * y3) - (z3 * z3);
			if (t3 < 0)
			{
				n3 = 0.0f;
			}
			else
			{
				t3 *= t3;
				n3 = t3 * t3 * Gradient(gi3, x3, y3, z3);
			}

			// Scales to [-1, 1]
			// Magic number needs source.
			// return 32.0f * (n0 + n1 + n2 + n3);

			// Scales to [0, 1]
			return ((32.0f * (n0 + n1 + n2 + n3)) + 1.0f) / 2.0f;
		}

		public float Octaves(int octaves, float x)
		{
			float
				output = 0.0f,
				denom = 0.0f,
				frequency = Frequency,
				amplitude = Amplitude;

			for (int i = 0; i < octaves; i++)
			{
				output += (amplitude * Generate(x * frequency));
				denom += amplitude;

				frequency *= Lacunarity;
				amplitude *= Persistence;
			}

			// Scales to [-1, 1]
			// return output / denom;

			// Scales to [0, 1]
			return output / denom + 1.0f / 2.0f;
		}

		public float Octaves(int octaves, float x, float y)
		{
			float
				output = 0.0f,
				denom = 0.0f,
				frequency = Frequency,
				amplitude = Amplitude;

			for (int i = 0; i < octaves; i++)
			{
				output += (amplitude * Generate(x * frequency, y * frequency));
				denom += amplitude;

				frequency *= Lacunarity;
				amplitude *= Persistence;
			}

			// Scales to [-1, 1]
			// return output / denom;

			// Scales to [0, 1]
			return output / denom + 1.0f / 2.0f;
		}

		public float Octaves(int octaves, float x, float y, float z)
		{
			float
				output = 0.0f,
				denom = 0.0f,
				frequency = Frequency,
				amplitude = Amplitude;

			for (int i = 0; i < octaves; i++)
			{
				output += (amplitude * Generate(x * frequency, y * frequency, z * frequency));
				denom += amplitude;

				frequency *= Lacunarity;
				amplitude *= Persistence;
			}

			// Scales to [-1, 1]
			// return output / denom;

			// Scales to [0, 1]
			return output / denom + 1.0f / 2.0f;
		}



		#region Factory
		public static class Factory
		{
			private static SimplexGenerator? Reference = null;
			
			public static SimplexGenerator ReferenceGenerator(
				float frequency = REFERENCE_FREQUENCY,
				float amplitude = REFERENCE_AMPLITUDE,
				float lacunarity = REFERENCE_LACUNARITY,
				float persistence = REFERENCE_PERSISTENCE
			) => Reference ??= new()
			{
				Permutation = ReferencePermutation,
				Frequency = frequency,
				Amplitude = amplitude,
				Lacunarity = lacunarity,
				Persistence = persistence
			};

			public static SimplexGenerator GeneratorFromSeed(
				int seed,
				float frequency = REFERENCE_FREQUENCY,
				float amplitude = REFERENCE_AMPLITUDE,
				float lacunarity = REFERENCE_LACUNARITY,
				float persistence = REFERENCE_PERSISTENCE
			)
			{
				Simulation.Random.CreateCustomGenerator(seed);

				SimplexGenerator simplex = new()
				{
					Permutation = GeneratePermutation(seed),
					Frequency = frequency,
					Amplitude = amplitude,
					Lacunarity = lacunarity,
					Persistence = persistence
				};

				Simulation.Random.DeleteCustomGenerator(seed);
				return simplex;
			}

			public static SimplexGenerator GeneratorFromRandomGenerator(
				int id,
				float frequency = REFERENCE_FREQUENCY,
				float amplitude = REFERENCE_AMPLITUDE,
				float lacunarity = REFERENCE_LACUNARITY,
				float persistence = REFERENCE_PERSISTENCE
			) => new()
			{
				Permutation = GeneratePermutation(id),
				Frequency = frequency,
				Amplitude = amplitude,
				Lacunarity = lacunarity,
				Persistence = persistence
			};
		}
		#endregion
	}
}
