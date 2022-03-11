using Fishbowl.Structures;
using Fishbowl.Util;
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
		private static void Setup()
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				Ansi.EnableWindowsAnsi();
			}

			ConsoleKeys.StartKeyListener();
			Diagnostics.StartWatch();
		}


		private static void Cleanup()
		{
			ConsoleKeys.CleanUpListener();
		}


		public static void Simulate(ulong loops = 0, uint timeout = 1000)
		{
			Diagnostics.StartStartUpTimer();
			// System Setup.
			Setup();

			bool forever = false;
			if (loops == 0) forever = true;

			// Setup

			Board board = new(70, 30);
			board.FillTerrainWithPerlin();

			// End Setup

			bool continuing = true;
			Diagnostics.EndStartUpTimer();
			while (continuing)
			{
				Console.Clear();
				Diagnostics.StartLoopTimer();
				if (Rules.ShowSimulationTime)
				{
					Console.WriteLine($"Loop runtime: {Diagnostics.LastLoopMilliseconds} ms");
				}

				// Start Actions

				board.DrawBoard();

				//if (ConsoleKeys.IsKeyWaiting)
				//{
				//	ConsoleKeyInfo? key = ConsoleKeys.GetConsoleKey();

				//	if (key is not null)
				//	{
				//		Console.WriteLine($"Key pressed! ({key.Value.KeyChar})");
				//	}
				//}

				// End Actions

				Diagnostics.StopLoopTimer();

				
				if (Diagnostics.LastLoopMilliseconds < timeout)
				{
					Thread.Sleep((int)(timeout - Diagnostics.LastLoopMilliseconds));
				}

				continuing = forever || --loops > 0;
			}

			Diagnostics.StartShutDownTimer();
			Cleanup();
			Diagnostics.EndShutDownTimer();
		}
	}
}
