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

		public string Name { get; init; }

		public RawInputRouter(string name)
		{
			Name = name;
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
