using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Sketches
{

	// Needs investigation for performance.
	// Testing scenario needed (debug + release -o).
#if false
	internal class AddIfNotInterned
	{
		if (StringUtil.TryGetIntern(key, out string? interned))
			Map.Add(interned!, obj);
		else
			Map.Add(string.Intern(key), obj);
	}
#endif
}
