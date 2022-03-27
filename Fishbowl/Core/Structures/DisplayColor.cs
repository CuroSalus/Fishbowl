using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	internal readonly struct DisplayColor
	{
		/// <summary>Red Value (0, 255)</summary>
		public readonly byte R;
		/// <summary>Green Value (0, 255)</summary>
		public readonly byte G;
		/// <summary>Blue Value (0, 255)</summary>
		public readonly byte B;

		public DisplayColor(byte r, byte g, byte b)
		{
			R = r;
			G = g;
			B = b;
		}

		#region Constants
		public static readonly DisplayColor Black = new(0, 0, 0);
		public static readonly DisplayColor White = new(255, 255, 255);
		public static readonly DisplayColor Red = new(255, 0, 0);
		public static readonly DisplayColor Green = new(0, 255, 0);
		public static readonly DisplayColor Blue = new(0, 0, 255);
		#endregion
	}
}
