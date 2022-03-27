using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	internal interface IInputComponent : IComponent
	{
		public abstract void Process(ConsoleKeyInfo? key);

		public abstract ConsoleKeyInfo? GetInput();
	}
}
