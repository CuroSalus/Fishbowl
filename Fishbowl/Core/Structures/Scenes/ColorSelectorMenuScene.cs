using Fishbowl.Core.Structures.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures.Scenes
{
	internal sealed class ColorSelectorMenuScene : Scene
	{
		public const string COLOR_PICKER_MENU_CONTROL = "COLOR_PICKER_MENU_CONTROL";
		readonly ColorPickerMenuControl menuControl;
		public override bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override int XPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override int YPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public ColorSelectorMenuScene()
			: base("Color Selector Menu")
		{
			menuControl = new(COLOR_PICKER_MENU_CONTROL);
			DisplayChildren.Add(menuControl);
		}

		public override void Process()
		{
			if (menuControl.IsComplete)
			{
				Console.WriteLine($"Color selected: {DisplayColor.GetPrettyString(menuControl.GetResult())}");
			}
		}

		protected override void HandleInput(ConsoleKeyInfo? keyInfo)
		{
			menuControl.Handle(keyInfo);
		}
	}
}
