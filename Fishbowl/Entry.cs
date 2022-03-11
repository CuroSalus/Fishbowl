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
			Simulation.Rules.ShowSimulationTime = true;
			Simulation.Simulate(1);

#if DEBUG
			Console.ReadKey();
			Console.Clear();

			ConsoleUtility.WritePermutation(Simulation.Blackboard.GetItem<PerlinGenerator>(PerlinGenerator.BLACKBOARD_PERLIN_REFERENCE)!.GetPermutation().ToArray());

			Console.WriteLine($"Startup time: {Simulation.Diagnostics.StartUpMilliseconds} ms");
			Console.WriteLine($"Shutdown time: {Simulation.Diagnostics.ShutDownMilliseconds} ms");
#endif

			return 0;
		}
	}
}
