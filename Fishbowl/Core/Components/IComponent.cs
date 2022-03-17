using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
	/// <summary>Represents the base Component for the Component/Composite pattern.</summary>
	internal interface IComponent
	{
		public string Name { get; init; }

//		protected IComponent(string name)
//		{
//#if DEBUG || INVARIANT
//			Invariant.Assert(string.IsInterned(name) is not null, $"Component names must be interned.");
//#endif
//			Name = name;
//		}
	}
}
