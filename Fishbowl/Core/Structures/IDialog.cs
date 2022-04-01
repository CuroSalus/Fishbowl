using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	internal interface IDialog { }

	internal interface IDialog<T> : IDialog
	{
		bool IsComplete { get; }
		T GetResult();
	}
}
