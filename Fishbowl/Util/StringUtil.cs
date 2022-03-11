using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Util
{
	internal static class StringUtil
	{
		public static bool TryGetIntern(string str, out string? interned)
		{
			interned = string.IsInterned(str);

			return interned is not null;
		}

	}
}
