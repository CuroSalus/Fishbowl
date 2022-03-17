using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
	public class ComponentException : Exception
	{
		internal ComponentException(string message) : base(message) { }

		internal ComponentException(string message, Exception innerException) : base(message, innerException) { }
	}
}
