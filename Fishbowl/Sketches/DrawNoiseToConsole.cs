using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbowl.Sketches
{
#if false
	internal class DrawNoiseToConsole
	{
			Random random = new();
			int seed = random.Next();

			IOctaveNoise gen = SimplexGenerator.Factory.GeneratorFromSeed(seed);

			int width = 110;
			int height = 25;
			Color[,] colors = new Color[width, height];

			for (int x = width - 1; x >= 0; x--)
			for (int y = height - 1; y >= 0; y--)
			{
				int[] color = new int[3];

				for (int z = 0; z < 3; z++)
				{
					float
						xin = (float)x / width -  (0.5f * width),
						yin = (float)y / height - (0.5f * height);

					float noise = gen.Octaves(
						128,
						xin,
						yin,
						(z+1) * 0.333333333333333f
					);

					if (noise > 1) noise = 1;
					if (noise < 0) noise = 0;

					color[z] = (int)(noise * 254.9);
				}

				colors[x,y] = Color.FromArgb(color[0], color[1], color[2]);
			}

			Colors.SaveCurrentColors();
			Simulation.Diagnostics.StartWatch();
			Thread.Sleep(100);

			Simulation.Diagnostics.StartLoopTimer();

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Console.Write($"{Ansi.StartBackgroundColor(colors[x, y])} ");
				}
				Console.WriteLine(Ansi.StartBackgroundColor(Colors.SavedBackground));
			}

			Simulation.Diagnostics.StopLoopTimer();

			Console.WriteLine($"Draw time: {Simulation.Diagnostics.LastLoopTicks} ticks ({Simulation.Diagnostics.LastLoopMilliseconds} ms); seed: {seed}");

			Colors.LoadSavedColors();
	}
#endif
}
