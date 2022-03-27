using Fishbowl.Core.Structures;
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
		public static DisplayColor SavedBackground { get; private set; }
		public static DisplayColor SavedForeground { get; private set; }

		public static DisplayColor CurrentBackground { get; private set; }
		public static DisplayColor CurrentForeground { get; private set; }

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

		public static void StartSavedColors()
		{
			Console.Write(Ansi.StartAllColor(SavedBackground, SavedForeground));
		}

		public static void StartCurrentColors()
		{
			Console.Write(Ansi.StartAllColor(CurrentBackground, CurrentForeground));
		}
	}
}
