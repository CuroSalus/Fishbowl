using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Fishbowl.Core
{
	internal static partial class Simulation
	{
		public static class Diagnostics
		{
			private static Stopwatch Watch = new();
			private static long StartUpStartTicks = 0;
			private static long StartUpEndTicks = 0;
			private static long ShutdownStartTicks = 0;
			private static long ShutdownEndTicks = 0;
			private static long CycleStartTicks = 0;
			private static long CycleStopTicks = 0;
			
			public static long ElapsedTicks => Watch.ElapsedTicks;
			public static long StartupTicks { get; private set; } = 0;
			public static long ShutdownTicks { get; private set; } = 0;
			public static long LastCycleMilliseconds => (long)(LastCycleTicks * SystemDiscovery.MillisecondsTickScalar);
			public static long LastCycleMicroseconds => (long)(LastCycleTicks * SystemDiscovery.MicrosecondsTickScalar);
			public static long LastCycleTicks { get; private set; } = 0;

			public static void StartWatch()
			{
				Watch = Stopwatch.StartNew();
			}


			public static void StartCycleTimer()
			{
				CycleStartTicks = Watch.ElapsedTicks;
			}

			public static void StopCycleTimer()
			{
				CycleStopTicks = Watch.ElapsedTicks;

				LastCycleTicks = CycleStopTicks - CycleStartTicks;
			}

			public static void StartStartUpTimer()
			{
				StartUpStartTicks = Watch.ElapsedTicks;
			}

			public static void EndStartUpTimer()
			{
				StartUpEndTicks = Watch.ElapsedTicks;

				StartupTicks = StartUpEndTicks - StartUpStartTicks;
			}

			public static void StartShutDownTimer()
			{
				ShutdownStartTicks = Watch.ElapsedTicks;
			}

			public static void EndShutDownTimer()
			{
				ShutdownEndTicks = Watch.ElapsedTicks;

				ShutdownTicks = ShutdownEndTicks - ShutdownStartTicks;
			}
		}
	}
}
