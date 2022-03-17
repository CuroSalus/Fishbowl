using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures
{
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	internal struct DisplayColor
	{
		[FieldOffset(0)]
		public int ARGB;

		[FieldOffset(0)]
		public byte A;
		[FieldOffset(1)]
		public byte R;
		[FieldOffset(2)]
		public byte G;
		[FieldOffset(3)]
		public byte B;
	}
}
