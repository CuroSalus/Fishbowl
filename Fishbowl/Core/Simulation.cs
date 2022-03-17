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
		private static class Control
		{
			public static bool Forever = false;
			public static bool Continuing = true;
			public static ulong Loops;
			public static uint Timeout;
		}

		private static void Setup(ulong loops, uint timeout)
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				Ansi.EnableWindowsAnsi();
			}

			Control.Loops = loops;
			Control.Timeout = timeout;
			if (loops == 0)
			{
				Control.Forever = true;
			}

			ConsoleKeys.StartKeyListener();
			Diagnostics.StartWatch();
		}

		private static void Cleanup()
		{
			Diagnostics.StartShutDownTimer();
			ConsoleKeys.CleanUpListener();
			Diagnostics.EndShutDownTimer(); 
		}

		private static void CycleStart()
		{
			Diagnostics.StartCycleTimer();
			if (Rules.ClearConsoleAtCycleStart)
			{
				Console.Clear();
			}

			if (Rules.ShowCycleTime)
			{
				Console.WriteLine($"Loop runtime: {Diagnostics.LastCycleMilliseconds} ms");
			}
		}

		private static void CycleEnd()
		{
			if (Diagnostics.LastCycleMilliseconds < Control.Timeout)
			{
				Thread.Sleep((int)(Control.Timeout - Diagnostics.LastCycleMilliseconds));
			}

			Control.Continuing = Control.Forever || --Control.Loops > 0;
			Diagnostics.StopCycleTimer();
		}


		public static void Simulate(ulong loops = 0, uint timeout = 1000)
		{
			Diagnostics.StartStartUpTimer();
			Setup(loops, timeout);

			Board board;
			{
				board = new(70, 30);
				board.FillTerrainWithPerlin();
			}

			Diagnostics.EndStartUpTimer();
			while (Control.Continuing)
			{
				CycleStart();

				// Handle input.

				// Simulate world.


				// Process Messages.
				ReadOnlyArray<Message> messages = Blackboard.GetMessages();
				for (int i = 0; i < Blackboard.MessageCount; i++)
				{
					// Process Messages.
				}

				// Simulate Scene.

				// Grab input.

				CycleEnd();
			}


			Cleanup();
		}
	}
}
