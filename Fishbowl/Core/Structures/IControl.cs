using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	internal interface IControl : IComponent
	{
		/// <summary>
		/// Handles a keypress. Returns true if the keypress was handled, false otherwise.
		/// </summary>
		/// <param name="keyInfo">Key press information.</param>
		/// <returns>Returns true if the keypress was handled, false otherwise.</returns>
		public abstract bool Handle(ConsoleKeyInfo? keyInfo);
		public bool IsSelected { get; set; }
	}
}
