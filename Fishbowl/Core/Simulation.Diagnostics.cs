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
			private static Stopwatch Watch = new Stopwatch();
			private static long StartUpStartTicks = 0;
			private static long StartUpEndTicks = 0;
			private static long ShutDownStartTicks = 0;
			private static long ShutDownEndTicks = 0;
			private static long LoopStartTicks = 0;
			private static long LoopStopTicks = 0;
			
			public static long ElapsedTicks => Watch.ElapsedTicks;
			public static long StartUpMilliseconds { get; private set; } = 0;
			public static long ShutDownMilliseconds { get; private set; } = 0;
			public static long LastLoopMilliseconds => (long)(LastLoopTicks * SystemDiscovery.MillisecondsTickScalar);
			public static long LastLoopMicroseconds => (long)(LastLoopTicks * SystemDiscovery.MicrosecondsTickScalar);
			public static long LastLoopTicks { get; private set; } = 0;

			public static void StartWatch()
			{
				Watch = Stopwatch.StartNew();
			}


			public static void StartLoopTimer()
			{
				LoopStartTicks = Watch.ElapsedTicks;
			}

			public static void StopLoopTimer()
			{
				LoopStopTicks = Watch.ElapsedTicks;

				LastLoopTicks = LoopStopTicks - LoopStartTicks;
			}

			public static void StartStartUpTimer()
			{
				StartUpStartTicks = Watch.ElapsedTicks;
			}

			public static void EndStartUpTimer()
			{
				StartUpEndTicks = Watch.ElapsedTicks;

				StartUpMilliseconds = (long)((StartUpEndTicks - StartUpStartTicks) * SystemDiscovery.MillisecondsTickScalar);
			}

			public static void StartShutDownTimer()
			{
				ShutDownStartTicks = Watch.ElapsedTicks;
			}

			public static void EndShutDownTimer()
			{
				ShutDownEndTicks = Watch.ElapsedTicks;

				ShutDownMilliseconds = (long)((ShutDownEndTicks - ShutDownStartTicks) * SystemDiscovery.MillisecondsTickScalar);
			}
		}
	}
}
