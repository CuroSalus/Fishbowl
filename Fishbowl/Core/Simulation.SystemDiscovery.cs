using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core
{
	internal static partial class Simulation
	{
		public static class SystemDiscovery
		{
			public static readonly bool IsHighResolutionTime = Stopwatch.IsHighResolution;
			public static readonly double NanosecondTickScalar = 1_000_000_000.0 / Stopwatch.Frequency;
			public static readonly double MicrosecondsTickScalar = NanosecondTickScalar / 1_000.0;
			public static readonly double MillisecondsTickScalar = MicrosecondsTickScalar / 1_000.0;
			public static readonly double SecondsTickScalar = MillisecondsTickScalar / 1_000.0;
		}
	}
}
