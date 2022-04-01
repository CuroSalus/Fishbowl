using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures.Input
{
	internal class RawInputRouter : IInputComponent
	{
		private ConsoleKeyInfo? KeyInfo;

		public string ComponentName { get; init; }

		public RawInputRouter(string name)
		{
			ComponentName = name;
		}

		public ConsoleKeyInfo? GetInput()
		{
			return KeyInfo;
		}

		public void Process(ConsoleKeyInfo? key)
		{
			KeyInfo = key;
		}
	}
}
