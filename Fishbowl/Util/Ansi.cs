using Fishbowl.Core.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Util
{
	internal static class Ansi
	{
		public const char ESC_CHAR = '\u001b';
		public const string Start = $"\u001b[";
		public static readonly string ResetEffects = $"{Start}0m";

		internal static class FontEffects
		{
			public const string BOLD_ON = "1";
			public const string BOLD_OFF = "2";
			public const string UNDERLINE_ON = "4";
			public const string UNDERLINE_OFF = "5";
		}

		public static string StartForegroundColor(byte r, byte g, byte b)
		{
			return $"{Start}38;2;{r};{g};{b}m";
		}

		public static string StartForegroundColor(DisplayColor color)
		{
			return StartForegroundColor(color.R, color.G, color.B);
		}

		public static string StartBackgroundColor(byte r, byte g, byte b)
		{
			return $"{Start}48;2;{r};{g};{b}m";
		}

		public static string StartBackgroundColor(DisplayColor color)
		{
			return StartBackgroundColor(color.R, color.G, color.B);
		}

		public static string StartCurrentForeground()
		{
			return StartForegroundColor(Colors.CurrentForeground);
		}

		public static string StartCurrrentBackground()
		{
			return StartBackgroundColor(Colors.CurrentBackground);
		}

		public static string StartAllColor(byte br, byte bg, byte bb, byte fr, byte fg, byte fb)
		{
			return $"{Start}38;{fr};{fg};{fb};48;{br};{bg};{bb}m";
		}

		public static string StartAllColor(DisplayColor background, DisplayColor foreground)
		{
			return StartAllColor(background.R, background.G, background.B, foreground.R, foreground.G, foreground.B);
		}

		public static string CustomAnsiControl(string control)
		{
			return $"{Start}{control}m";
		}

		#region WINANSI
		public static void EnableWindowsAnsi()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new InvalidOperationException("WIN ANSI: CANNOT ENABLE WIN32 ANSI ON SYSTEMS OTHER THAN WINDOWS");
			}

			IntPtr iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
			if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
			{
				throw new InvalidOperationException($"WIN ANSI: UNABLE TO GET CONSOLE MODE. \n\t{GetLastError()}");
			}

			outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
			if (!SetConsoleMode(iStdOut, outConsoleMode))
			{
				throw new InvalidOperationException($"WIN ANSI: UNABLE TO SET CONSOLE MODE. \n\t{GetLastError()}");
			}
		}

		private const int STD_OUTPUT_HANDLE = -11;
		private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
		private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;
		[DllImport("kernel32.dll")]
		private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

		[DllImport("kernel32.dll")]
		private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll")]
		public static extern uint GetLastError();
		#endregion
	}
}
