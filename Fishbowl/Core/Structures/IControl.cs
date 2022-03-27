using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	internal interface IControl : IComponent
	{
		public abstract void Handle(ConsoleKeyInfo? keyInfos);
		public bool IsSelected { get; set; }
	}
}
