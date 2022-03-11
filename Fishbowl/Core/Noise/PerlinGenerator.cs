using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fishbowl.Util;

namespace Fishbowl.Core.Noise
{
	internal class PerlinGenerator : EngineObject, INoise
	{
		public const string BLACKBOARD_PERLIN_REFERENCE = $"BLACKBOARD_{nameof(PerlinGenerator)}";
		public const string BLACKBOARD_PERLIN_SEED = $"{BLACKBOARD_PERLIN_REFERENCE}-SEED";

		#region Static
		private static byte[] GeneratePermutation()
		{
			byte[] data = new byte[256];

			LinkedList<byte> scrambleList	= new();

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

		private static float Fade(float dub) => ((dub * 6 - 15) * dub + 10) * dub * dub * dub;

		private static float LinearInterpolation(float t, float x, float y) => x + t * (y - x);

		private static float Gradient(int hash, float x, float y, float z)
		{
			return (hash & 0x0F) switch
			{
				0x0 =>  x + y,
				0x1 => -x + y,
				0x2 =>  x - y,
				0x3 => -x - y,
				0x4 =>  x + z,
				0x5 => -x + z,
				0x6 =>  x - z,
				0x7 => -x - z,
				0x8 =>  y + z,
				0x9 => -y + z,
				0xA =>  y - z,
				0xB => -y - z,
				0xC =>  y + x,
				0xD => -y + z,
				0xE =>  y - x,
				0xF => -y - z,
				_   =>      0, // This will never be hit. Microsoft, numerical domain analysis pls.
			};
		}

		public static readonly ReadOnlyArray<byte> ReferencePermutation = ReadOnlyArray<byte>.CreateByArrayReference(new byte[256]
		{
			151, 160, 137,  91,  90,  15, 131,  13, 201,  95,  96,  53, 194, 233,   7, 225,
			140,  36, 103,  30,  69, 142,   8,  99,  37, 240,  21,  10,  23, 190,   6, 148,
			247, 120, 234,  75,   0,  26, 197,  62,  94, 252, 219, 203, 117,  35,  11,  32,
			 57, 177,  33,  88, 237, 149,  56,  87, 174,  20, 125, 136, 171, 168,  68, 175,
			 74, 165,  71, 134, 139,  48,  27, 166,	 77, 146, 158, 231,  83, 111, 229, 122,
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
		#endregion



		private ReadOnlyArray<byte> Permutation { get; init; }

		private PerlinGenerator()
			: base(BLACKBOARD_PERLIN_REFERENCE) {
			Permutation = Array.Empty<byte>();
		}

		private byte Permute(int index) => Permutation[(byte)index];

		public ReadOnlyArray<byte> GetPermutation() => Permutation;
		float INoise.Generate(float x) { throw new NotImplementedException(); }
		float INoise.Generate(float x, float y) { throw new NotImplementedException();  }

		/// <summary>Returns a value from 0.0  -1.0</summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		public float Generate(float x, float y, float z)
		{
			// Unit square
			int
				sq_x = (int)x & 255,
				sq_y = (int)y & 255,
				sq_z = (int)z & 255;
			
			// Retrieve only decimal portion
			x -= (int)x;
			y -= (int)y;
			z -= (int)z;

			// Create fading scalar
			float
				u = Fade(x),
				v = Fade(y),
				w = Fade(z);

			int
				A  = Permute(sq_x    ) + sq_y,
				AA = Permute(A       ) + sq_z,
				AB = Permute(A + 1   ) + sq_z,
				B  = Permute(sq_x + 1) + sq_y,
				BA = Permute(B       ) + sq_z,
				BB = Permute(B + 1   ) + sq_z;

			// Look upon and weep, for readability has perished.
			float value = LinearInterpolation(w,
				LinearInterpolation(v,
					LinearInterpolation(u,
						Gradient(Permute(AA  ),   x,   y,   z),
						Gradient(Permute(BA  ), x-1,   y,   z)
					),
					LinearInterpolation(u,
						Gradient(Permute(AB  ),   x, y-1,   z),
						Gradient(Permute(BB  ), x-1,   y,   z)
					)
				),
				LinearInterpolation(v,
					LinearInterpolation(u,
						Gradient(Permute(AA+1),   x,   y, z-1),
						Gradient(Permute(BA+1), x-1,   y, z-1)
					),
					LinearInterpolation(u,
						Gradient(Permute(AB+1),   x, y-1, z-1),
						Gradient(Permute(BB+1), x-1, y-1, z-1)
					)
				)
			);

			return value + 1.0f / 2.0f;
		}



		#region Factory
		public static class Factory
		{
			private static PerlinGenerator? Reference = null;
			public static PerlinGenerator ReferenceGenerator() {
				Reference ??= new() { Permutation = ReferencePermutation };
				Simulation.Blackboard.SetItem(BLACKBOARD_PERLIN_SEED, null);
				return Reference;
			}
			public static PerlinGenerator GeneratorFromSeed(int seed)
			{
				Simulation.Random.CreateCustomGenerator(seed);

				PerlinGenerator perlin = new() { Permutation = GeneratePermutation(seed) };

				Simulation.Random.DeleteCustomGenerator(seed);

				Simulation.Blackboard.SetItem(BLACKBOARD_PERLIN_SEED, seed);
				return perlin;
			}
			public static PerlinGenerator GeneratorFromRandomGenerator(int id) {
				Simulation.Blackboard.SetItem(BLACKBOARD_PERLIN_SEED, id);
				return new() { Permutation = GeneratePermutation(id) };
			}
		}
		#endregion
	}
}
