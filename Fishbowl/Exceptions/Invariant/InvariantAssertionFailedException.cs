using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
#if DEBUG || INVARIANT
	public class InvariantAssertionFailedException : InvariantAssertionException
	{
		internal InvariantAssertionFailedException(string message) : base(message) { }
		internal InvariantAssertionFailedException(string message, Exception innerException) : base(message, innerException) { }
	}
#endif
}
