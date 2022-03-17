using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
#if DEBUG || INVARIANT
	public class InvariantAssertionException : Exception
	{
		internal InvariantAssertionException(string message) : base(message) { }
		internal InvariantAssertionException(string message, Exception innerException) : base(message, innerException) { }
	}
#endif
}
