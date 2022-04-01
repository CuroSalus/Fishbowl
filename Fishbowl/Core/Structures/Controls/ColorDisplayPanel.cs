using Fishbowl.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures.Controls
{
	internal class ColorDisplayPanel : EngineObject, IDisplayComponent
	{
		public DisplayColor Color;
		public int Width;
		public int Height;
		public ColorDisplayPanel(string name, int width, int height, DisplayColor? color = null)
		{
			ComponentName = name;
			Width = width;
			Height = height;
			Color = color ?? DisplayColor.Black;
			Enabled = true;
		}
		
		public bool Enabled { get; set; }
		public int XPosition { get; set; }
		public int YPosition { get; set; }
		public string ComponentName { get; init; }

		public void AddChild(IDisplayComponent child) => throw new NotImplementedException();
		public void RemoveChild(IDisplayComponent child) => throw new NotImplementedException();

		public void Draw()
		{
			if (!Enabled) return;
			Console.SetCursorPosition(XPosition, YPosition);

			Console.Write(Ansi.StartAllColor(Color, Color));

			for (int y = 0; y < Height && y < Screen.Height; y++)
			{
				Console.Write(new string(' ', Width));
			}
		}

	}
}
