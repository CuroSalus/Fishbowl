using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core
{
	internal static partial class Simulation
	{
		public static class Rules
		{
			/// <summary>Display the engine's last Cycle time.</summary>
			public static bool ShowCycleTime = false;
			/// <summary>Clear the console at Cycle start.</summary>
			public static bool ClearConsoleAtCycleStart = true;
		}
	}
}
