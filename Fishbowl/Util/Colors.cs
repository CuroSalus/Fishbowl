using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Util
{
	internal class Colors
	{
		public static Color SavedBackground { get; private set; }
		public static Color SavedForeground { get; private set; }

		public static Color CurrentBackground { get; private set; }
		public static Color CurrentForeground { get; private set; }

		public static void SaveCurrentColors()
		{
			SavedBackground = CurrentBackground;
			SavedForeground = CurrentForeground;
		}

		public static void LoadSavedColors()
		{
			CurrentBackground = SavedBackground;
			CurrentForeground = SavedForeground;
		}
	}
}
