using Fishbowl.Core.Structures;
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
			public static Scene? CurrentScene;
		}
		
		public static void EndSimulation()
		{
			Control.CurrentScene = null;
		}

		private static void Setup(string[] args, ulong loops, uint timeout)
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				Ansi.EnableWindowsAnsi();
			}
			Console.CursorVisible = false;
			Screen.Height = Console.WindowHeight;
			Screen.Width = Console.WindowWidth;


			Control.Loops = loops;
			Control.Timeout = timeout;
			if (loops == 0)
			{
				Control.Forever = true;
			}

			Control.CurrentScene = new Structures.Scenes.ColorSelectorMenuScene();
			
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
				Console.WriteLine($"Loop runtime: {Diagnostics.LastCycleMicroseconds} {StringUtil.Symbols.LOWERCASE_MU}s");
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


		public static void Simulate(string[] args, ulong loops = 0, uint timeout = 1000)
		{
			Diagnostics.StartStartUpTimer();
			Setup(args, loops, timeout);

			//Board board;
			//{
			//	board = new(70, 30);
			//	board.FillTerrainWithPerlin();
			//}

			Diagnostics.EndStartUpTimer();
			while (Control.Continuing)
			{
				CycleStart();

				// Handle Input
				if (ConsoleKeys.IsKeyWaiting)
				{
					Control.CurrentScene?.ProcessInput(ConsoleKeys.GetConsoleKey());
				}

				// Simulate world.
				World.Simulate();

				// Simulate Scene.
				Control.CurrentScene?.Process();

				// Process Messages.
				ReadOnlyArray<Message> messages = Blackboard.Messages;
				for (int i = 0; i < Blackboard.MessageCount; i++)
				{
					// Process Messages.
				}
				Blackboard.ClearMessages();

				// Render Scene.
				Control.CurrentScene?.Draw();

				Thread.Sleep(100);
				
				CycleEnd();
			}


			Cleanup();
		}
	}
}
