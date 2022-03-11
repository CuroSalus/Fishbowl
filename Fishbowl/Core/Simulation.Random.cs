using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRandom = System.Random;

namespace Fishbowl.Core
{
	internal static partial class Simulation
	{
		public static class Random
		{
			private static SystemRandom Generator;
			private static Dictionary<int, SystemRandom> CustomGenerators;
			public static int Seed { get; private set; }
			static Random()
			{
				CustomGenerators = new();
				SystemRandom random = new SystemRandom();
				Seed = random.Next();
				Generator = new SystemRandom(Seed);
			}

			public static void SetSeed(int seed)
			{
				Seed = seed;
				Generator = new SystemRandom(Seed);
			}

			public static int GetInt()
			{
				return Generator.Next();
			}
			public static int GetInt(int max)
			{
				return Generator.Next(max);
			}

			public static int GetInt(int min, int max)
			{
				return Generator.Next(min, max);
			}

			public static int CreateCustomGenerator()
			{
				int seed = GetInt();
				CustomGenerators[seed] = new SystemRandom(seed);
				return seed;
			}

			public static int CreateCustomGenerator(int seed)
			{
				CustomGenerators[seed] = new SystemRandom(seed);
				return seed;
			}

			public static void DeleteCustomGenerator(int seed)
			{
				if (!CustomGenerators.Remove(seed))
				{
					throw new InvalidOperationException("A generator id was supplied to be removed but there was not a generator that matches.");
				}
			}

			public static int CustomGetInt(int id)
			{
				return CustomGenerators[id].Next();
			}
			public static int CustomGetInt(int id, int max)
			{
				return CustomGenerators[id].Next(max);
			}
			public static int CustomGetInt(int id, int min, int max)
			{
				return CustomGenerators[id].Next(min, max);
			}
		}
	}
}
