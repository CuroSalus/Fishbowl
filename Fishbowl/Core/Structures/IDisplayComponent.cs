using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	internal interface IDisplayComponent : IComponent
	{
		public bool Enabled { get; set; }
		public int XPosition { get; set; }
		public int YPosition { get; set; }

		public abstract void Draw();

		public void AddChild(IDisplayComponent child);

		public void RemoveChild(IDisplayComponent child);
	}
}
