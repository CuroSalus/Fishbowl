using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl
{
	public static class Invariant
	{
		public static void Assert(bool condition)
		{
#if DEBUG || INVARIANT
			if (!condition) throw new InvariantAssertionFailedException("An assertion failed!");
#endif
		}

		public static void Assert(bool condition, string message)
		{
#if DEBUG || INVARIANT
			if (!condition) throw new InvariantAssertionFailedException(message);
#endif
		}

		public static void Assert(Func<bool> condition)
		{
#if DEBUG || INVARIANT
			Assert(condition());
#endif
		}

		public static void Assert(Func<bool> condition, string message)
		{
#if DEBUG || INVARIANT
			Assert(condition(), message);
#endif
		}
	}
}
