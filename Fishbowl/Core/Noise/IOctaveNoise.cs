using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Noise
{
	/// <summary>A noise generator capable of perform octave-based noise generation.</summary>
	internal interface IOctaveNoise : INoise
	{
		float Octaves(int octaves, float x);
		float Octaves(int octaves, float x, float y);
		float Octaves(int octaves, float x, float y, float z);
	}
}
