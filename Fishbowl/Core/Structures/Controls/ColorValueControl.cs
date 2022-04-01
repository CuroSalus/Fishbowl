using Fishbowl.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures.Controls
{
	internal class ColorValueControl : EngineObject, IControl, IDisplayComponent, IControlLeaf
	{
		public enum Color
		{
			Red,
			Blue,
			Green
		}

		public const string LABEL_NAME = "LABEL";
		public const string VALUE_NAME = "VALUE";

		private readonly TextControl LabelControl;
		private readonly TextControl ValueControl;

		public byte Value { get; set; }
		public ColorValueControl.Color ControlColor { get; private set; }
		public DisplayColor LabelForegroundColor { get; set; }
		public DisplayColor LabelBackgroundColor { get; set; }

		public string ComponentName { get; init; }
		public ColorValueControl(string name, ColorValueControl.Color color, int xpos, int ypos)
		{
			ComponentName = name;
			Enabled = true;
			ControlColor = color;
			XPosition = xpos;
			YPosition = ypos;

			switch (color)
			{
				case ColorValueControl.Color.Red:
					Components[LABEL_NAME] = LabelControl = new TextControl(LABEL_NAME, xpos, ypos, "Red:") { Width = 6, Height = 1 };
					Components[VALUE_NAME] = ValueControl = new TextControl(VALUE_NAME, xpos + 3, ypos + 1, Value, null, DisplayColor.Red) { Width = 6, Height = 1 };
					break;
				case ColorValueControl.Color.Green:
					Components[LABEL_NAME] = LabelControl = new TextControl(LABEL_NAME, xpos, ypos, "Green:") { Width = 6, Height = 1 };
					Components[VALUE_NAME] = ValueControl = new TextControl(VALUE_NAME, xpos + 3, ypos + 1, Value, null, DisplayColor.Green) { Width = 6, Height = 1 };
					break;
				case ColorValueControl.Color.Blue:
					Components[LABEL_NAME] = LabelControl = new TextControl(LABEL_NAME, xpos, ypos, "Blue:") { Width = 6, Height = 1 };
					Components[VALUE_NAME] = ValueControl = new TextControl(VALUE_NAME, xpos + 3, ypos + 1, Value, null, DisplayColor.Blue) { Width = 6, Height = 1 };
					break;
				default:
					Components[LABEL_NAME] = LabelControl = new TextControl(LABEL_NAME, xpos, ypos, "UNDFND") { Width = 6, Height = 1 };
					Components[VALUE_NAME] = ValueControl = new TextControl(VALUE_NAME, xpos + 3, ypos + 1, Value) { Width = 6, Height = 1 };
					break;
			}
			
		}

		#region Interface: IDisplayComponent
		public bool Enabled { get; set; }
		public int XPosition { get; set; }
		public int YPosition { get; set; }

		public void Draw()
		{
			if (Enabled)
			{
				if (IsSelected)
				{
					LabelControl.Foreground = DisplayColor.Black;
					LabelControl.Background = DisplayColor.White;
					LabelControl.Draw();
				}
				else
				{
					LabelControl.Foreground = DisplayColor.White;
					LabelControl.Background = DisplayColor.Black;
					LabelControl.Draw();
				}
				ValueControl.Text = Value.ToString();
				ValueControl.Draw();
			}
		}
		
		public void RemoveChild(IDisplayComponent child) => throw new NotImplementedException();
		public void AddChild(IDisplayComponent child) => throw new NotImplementedException();
		#endregion

		#region Interace: IControl
		public bool IsSelected { get; set; }

		/// <summary>
		/// Handles key presses. Up to increment. Down to decrement.
		/// Always returns true as it's functionally leaf element with no children.
		/// </summary>
		/// <param name="keyInfo"></param>
		/// <returns></returns>
		public bool Handle(ConsoleKeyInfo? keyInfo)
		{
			if (keyInfo is null || !IsSelected) return true;

			switch (keyInfo.Value.Key)
			{
				case ConsoleKey.UpArrow:
					Value++;
					break;
				case ConsoleKey.DownArrow:
					Value--;
					break;
			}

			return true;
		}
		#endregion
	}
}
