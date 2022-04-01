using Fishbowl.Core.Structures.Controls;
using Fishbowl.Core.Structures.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures.Controls
{
	internal class ColorPickerMenuControl : EngineObject, IControl, IDisplayComponent, IDialog<DisplayColor>
	{
		public const string COLOR_PICKER_TITLE = "COLOR_PICKER_TITLE";
		public const string RED_COLOR_CONTROL = "RED_COLOR_CONTROL";
		public const string GREEN_COLOR_CONTROL = "GREEN_COLOR_CONTROL";
		public const string BLUE_COLOR_CONTROL = "BLUE_COLOR_CONTROL";
		public const string INSTRUCTIONS_NAME = "INSTRUCTIONS_NAME";
		public const string COLOR_DISPLAY_CONTROL = "COLOR_DISPLAY_CONTROL";
		private const string INSTRUCTIONS_TEXT = "Left/Right to change color, Up/Down to change intensity, Enter/Space to select color.";

		private readonly IDisplayComponent ScreenTitle;
		private readonly IDisplayComponent Instructions;
		private readonly ColorValueControl RedValueControl;
		private readonly ColorValueControl GreenValueControl;
		private readonly ColorValueControl BlueValueControl;
		private readonly ColorDisplayPanel ColorDisplayControl;
		private IControl SelectedControl;
		private DisplayColor CurrentColor;

		public ColorPickerMenuControl(string name)
		{
			ComponentName = name;
			ScreenTitle = new TextControl(COLOR_PICKER_TITLE, XPosition, YPosition, "Color Picker:") { Width = Screen.Width, Height = 1, };
			RedValueControl = new ColorValueControl(RED_COLOR_CONTROL, ColorValueControl.Color.Red, XPosition + 5, YPosition + 5);
			GreenValueControl = new ColorValueControl(GREEN_COLOR_CONTROL, ColorValueControl.Color.Green, XPosition + 12, YPosition + 5);
			BlueValueControl = new ColorValueControl(BLUE_COLOR_CONTROL, ColorValueControl.Color.Blue, XPosition + 19, YPosition + 5);
			ColorDisplayControl = new ColorDisplayPanel(COLOR_DISPLAY_CONTROL, XPosition + 26, YPosition + 5, DisplayColor.Black);
			Instructions = new TextControl(INSTRUCTIONS_NAME, 0, Screen.Height - 1, INSTRUCTIONS_TEXT) { Width = Screen.Width, Height = 1 };
			SelectedControl = RedValueControl;
			RedValueControl.IsSelected = true;

			Components.AddRange(new IComponent[]
				{
					ScreenTitle,
					RedValueControl,
					GreenValueControl,
					BlueValueControl,
					ColorDisplayControl,
					Instructions,
				},
				control => control.ComponentName
			);
		}

		#region Interface: IDialog<T>
		public bool IsComplete { get; set; }
		/// <summary>Returns the Dialog Result.</summary>
		/// <exception cref="InvalidOperationException">Thrown when the dialog is not complete.</exception>
		public DisplayColor GetResult()
		{
			if (IsComplete)
				return CurrentColor;
			else
				throw new InvalidOperationException("Color Picker is not complete.");
		}
		#endregion

		public int XPosition { get => 0; set => throw new NotImplementedException(); }
		public int YPosition { get => 0; set => throw new NotImplementedException(); }
		public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string ComponentName { get; init; }

		public void Draw()
		{
			ScreenTitle.Draw();
			RedValueControl.Draw();
			GreenValueControl.Draw();
			BlueValueControl.Draw();
			ColorDisplayControl.Draw();
			Instructions.Draw();
		}

		public bool Handle(ConsoleKeyInfo? keyInfo)
		{
			if (keyInfo == null)
				return false;

			bool handled = false;

			if (SelectedControl is null)
			{
				SelectedControl = RedValueControl;
			}

			switch (keyInfo?.Key)
			{
				case ConsoleKey.LeftArrow:
					CycleSelectedColorLeft();
					handled = true;
					break;
				case ConsoleKey.RightArrow:
					CycleSelectedColorRight();
					handled = true;
					break;
				case ConsoleKey.Enter:
				case ConsoleKey.Spacebar:
					IsComplete = true;
					break;
				default:
					handled = SelectedControl.Handle(keyInfo);
					break;
			}

			CurrentColor = new DisplayColor(RedValueControl.Value, GreenValueControl.Value, BlueValueControl.Value);
			return handled;
		}

		private void CycleSelectedColorRight()
		{
			SelectedControl.IsSelected = false;
			if (SelectedControl == RedValueControl)
			{
				SelectedControl = GreenValueControl;
			}
			else if (SelectedControl == GreenValueControl)
			{
				SelectedControl = BlueValueControl;
			}
			else if (SelectedControl == BlueValueControl)
			{
				SelectedControl = RedValueControl;
			}
			SelectedControl.IsSelected = true;
		}

		private void CycleSelectedColorLeft()
		{
			SelectedControl.IsSelected = false;
			if (SelectedControl == RedValueControl)
			{
				SelectedControl = BlueValueControl;
			}
			else if (SelectedControl == GreenValueControl)
			{
				SelectedControl = RedValueControl;
			}
			else if (SelectedControl == BlueValueControl)
			{
				SelectedControl = GreenValueControl;
			}
			SelectedControl.IsSelected = true;
		}

		public bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public void AddChild(IDisplayComponent child) => throw new NotImplementedException();
		public void RemoveChild(IDisplayComponent child) => throw new NotImplementedException();
	}
}
