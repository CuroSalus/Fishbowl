using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
	public class ComponentNotInCompositeException : ComponentException
	{
		internal ComponentNotInCompositeException(string componentId, int compositeId) : base($"The desired component ({componentId}) is not in the composite ({compositeId}).") { }
	}
}
