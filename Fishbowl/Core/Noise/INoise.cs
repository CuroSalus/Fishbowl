using Fishbowl.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Noise
{
	/// <summary>A noise generator for 1, 2, or 3 dimensions.</summary>
	internal interface INoise
	{
		float Generate(float x);
		float Generate(float x, float y);
		float Generate(float x, float y, float z);

		/// <summary>Return the permutation array used to generate noise.</summary>
		/// <returns>Byte array of length 256.</returns>
		ReadOnlyArray<byte> GetPermutation();
	}
}
