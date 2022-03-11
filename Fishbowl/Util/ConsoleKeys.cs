using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Util
{
	internal class ConsoleKeys
	{
		private static readonly CancellationTokenSource KeyStrokeCancelSource = new();
		private static Task? Manager;
		private static ConsoleKeyInfo? GrabbedKey = null;
		private static ConsoleKeyInfo? LastKey = null;
		private static readonly Func<ConsoleKeyInfo> ListenerAction = () =>
		{
			return Console.ReadKey(true);
		};
		private static readonly Action ManagerAction = async () =>
		{
			while (!KeyStrokeCancelSource.IsCancellationRequested)
			{
				ConsoleKeyInfo result = await Task.Run(ListenerAction, KeyStrokeCancelSource.Token);
				GrabbedKey = result;
			}
		};

		public static void StartKeyListener()
		{
			Manager = Task.Run(ManagerAction);
		}

		public static bool IsKeyWaiting { get => GrabbedKey != null; }
		public static ConsoleKeyInfo? GetConsoleKey()
		{
			ConsoleKeyInfo? key = GrabbedKey;
			LastKey = key ?? LastKey;
			GrabbedKey = null;
			return key;
		}

		public static void CleanUpListener()
		{
			KeyStrokeCancelSource.Cancel();
			if (Manager is not null) Manager.Wait();
		}
	}
}
