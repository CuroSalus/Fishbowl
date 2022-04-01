using Fishbowl.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures.Controls
{
	internal class TextControl : EngineObject, IControl, IDisplayComponent
	{
		public string? Text { get; set; }
		public DisplayColor Foreground { get; set; }
		public DisplayColor Background { get; set; }
		//public bool MoveWholeWords { get; set; }

		public TextControl(string name, int xpos, int ypos, string? text = null, DisplayColor? bg = null, DisplayColor? fg = null)
		{
			ComponentName = name;
			XPosition = xpos;
			YPosition = ypos;
			Background = bg ?? DisplayColor.Black;
			Foreground = fg ?? DisplayColor.White;
			Text = text;
			Enabled = true;
		}

		public TextControl(string name, int xpos, int ypos, object? text = null, DisplayColor? bg = null, DisplayColor? fg = null)
			: this(name, xpos, ypos, text?.ToString(), bg, fg)
		{}

		public int Width { get; set; }
		public int Height { get; set; }

		#region Interface: IControl
		public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string ComponentName { get; init; }
		#endregion

		#region Interface: IDisplayComponent
		public bool Enabled { get; set; }
		public int XPosition { get; set; }
		public int YPosition { get; set; }


		public void Draw()
		{
			if (!Enabled || Text is null) return;

			if (XPosition > Screen.Width || YPosition > Screen.Height)
				return;

			Console.SetCursorPosition(XPosition, YPosition);

			Console.Write(Ansi.StartAllColor(Background, Foreground));

			string[] words = Text.Split(' ');

			foreach (string word in words)
			{
				if (word.Length + Console.CursorLeft > (XPosition + Width))
				{
					//Console.WriteLine();
					Console.SetCursorPosition(XPosition, Console.CursorTop + 1);
				}

				//for (int i = 0; i < word.Count(c => c == '\n'); i++)
				//{
				//	//Console.WriteLine();
				//	Console.SetCursorPosition(XPosition, Console.CursorTop + 1);
				//}

				// Break if the next line would be out of bounds
				//if (word.Length + Console.CursorLeft > Screen.Height)
				//	break;

				Console.Write($"{word} ");
			}

			Colors.StartCurrentColors();
		}
		
		bool IControl.Handle(ConsoleKeyInfo? keyInfos) => throw new NotImplementedException();
		void IDisplayComponent.AddChild(IDisplayComponent child) => throw new NotImplementedException();
		void IDisplayComponent.RemoveChild(IDisplayComponent child) => throw new NotImplementedException();
		#endregion

	}
}
