using Fishbowl.Core.Structures.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Core.Structures.Scenes
{
	internal class ColorPickerMenu : Scene
	{
		private byte Red;
		private byte Green;
		private byte Blue;
		private IDisplayComponent ScreenTitle;
		private IControl RedValueControl;
		private IControl GreenValueControl;
		private IControl BlueValueControl;
		private IControl FinalColorValueControl;
		private RawInputRouter InputListener;

		public ColorPickerMenu() : base("Color Picker")
		{
			Red = 255;
			Green = 255;
			Blue = 255;
		}

		public override void Process()
		{
			throw new NotImplementedException();
		}


	}
}
