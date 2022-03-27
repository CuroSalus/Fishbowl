using Fishbowl.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures.Controls
{
	internal class ColorValueControl : EngineObject, IControl, IDisplayComponent
	{
		public enum Color
		{
			Red,
			Blue,
			Green
		}

		public byte Value { get; set; }
		public string Label { get; set; }
		public ColorValueControl.Color ControlColor { get; private set; }
		public DisplayColor LabelForegroundColor { get; set; }
		public DisplayColor LabelBackgroundColor { get; set; }

		public string Name { get; init; }
		public ColorValueControl(string name, ColorValueControl.Color color)
		{
			Name = name;
			Enabled = true;
			ControlColor = color;

			switch (color)
			{
				case ColorValueControl.Color.Red:
					Label = "Red";
					break;
				case ColorValueControl.Color.Green:
					Label = "Green";
					break;
				case ColorValueControl.Color.Blue:
					Label = "Blue";
					break;
				default:
					Label = "UNDEFINED";
					break;
			}
		}

		#region Interface: IDisplayComponent
		public bool Enabled { get; set; }
		public int XPosition { get; set; }
		public int YPosition { get; set; }

		public void Draw()
		{
			ConsoleWriter writer = new(XPosition, YPosition);

			writer.Write(Ansi.StartAllColor(DisplayColor.Black, DisplayColor.White));
			writer.Write(Label);
			
			if (Enabled)
			{

				Console.SetCursorPosition(XPosition, YPosition);

				if (Value > 99)
				{
					Console.Write($" {Value}");
				}
				else
				{
					Console.Write(Value);
				}

				Colors.StartCurrentColors();
			}
		}
		public void RemoveChild(IDisplayComponent child) => throw new NotImplementedException();
		public void AddChild(IDisplayComponent child) => throw new NotImplementedException();
		#endregion

		#region Interace: IControl
		public bool IsSelected { get; set; }

		public void Handle(ConsoleKeyInfo? keyInfo)
		{
			if (keyInfo is null) return;

			switch (keyInfo.Value.Key)
			{
				case ConsoleKey.UpArrow:
					Value++;
					return;
				case ConsoleKey.DownArrow:
					Value++;
					return;
			}
			return;
		}
		#endregion
	}
}
