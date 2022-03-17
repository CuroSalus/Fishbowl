using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fishbowl.Core;
using Fishbowl.Core.Noise;
using Fishbowl.Util;

namespace Fishbowl
{
	internal class Entry
	{
		public static int Main(string[] _)
		{
			Simulation.Rules.ShowCycleTime = true;
			Simulation.Simulate(1);

#if DEBUG
			Console.ReadKey();
			Console.Clear();

			Console.WriteLine("DEBUG SCREEN:");

			ConsoleUtility.WritePermutation(Simulation.Blackboard.GetItem<PerlinGenerator>(PerlinGenerator.BLACKBOARD_PERLIN_REFERENCE)!.GetPermutation().ToArray());

			Console.WriteLine($"Startup time: {Simulation.Diagnostics.StartupTicks} ticks");
			Console.WriteLine($"Shutdown time: {Simulation.Diagnostics.ShutdownTicks} ticks");
#endif

			return 0;
		}
	}
}
