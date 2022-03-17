using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fishbowl.Core;
using Fishbowl.Core.Noise;
using Fishbowl.Util;

namespace Fishbowl
{
	public enum TerrainTypes : int
	{
		Dirt = 0,
		Water = 1,
		Grass = 2,
		Field = 3,
		Road = 4,
	}

	internal class Board
	{
		public readonly int XWidth;
		public readonly int YWidth;

		public readonly int[,] Terrain;
		public readonly int[,] Entities;

		public Board(int x = 30, int y = 30)
		{
			XWidth = x;
			YWidth = y;
			Terrain = new int[x, y];
			Entities = new int[x, y];
		}

		public void FillRandomTerrain()
		{
			for (int x = XWidth-1; x >= 0; x--)
			{
				for (int y = YWidth-1; y >= 0; y--)
				{
					Terrain[x, y] = Simulation.Random.GetInt(0, 5);
				}
			}
		}

		public void FillTerrain(TerrainTypes type)
		{
			for (int x = XWidth - 1; x >= 0; x--)
			{
				for (int y = YWidth - 1; y >= 0; y--)
				{
					Terrain[x, y] = (int)type;
				}
			}
		}

		public void FillTerrainWithPerlin()
		{
			PerlinGenerator noiseGenerator = PerlinGenerator.Factory.ReferenceGenerator();

			for (int x = XWidth - 1; x >= 0; x--)
			{
				for (int y = YWidth - 1; y >= 0; y--)
				{
					float
						xin = (float)x / XWidth,
						yin = (float)y / YWidth,
						zin = xin + yin;

					float noise = noiseGenerator.Generate(
						xin,
						yin,
						zin
					);
					Terrain[x, y] = (int)(noise * 4.999999999999999999999);
				}
			}
		}

		public void DrawBoard()
		{
			Colors.SaveCurrentColors();

			for (int y = 0; y < YWidth; y++)
			{
				for (int x = 0; x < XWidth; x++)
				{
					int cellTerrain = Terrain[x, y];

					switch ((TerrainTypes)cellTerrain)
					{
						case TerrainTypes.Dirt:
							if (Colors.CurrentBackground != Color.Brown)
							{
								Console.Write(Ansi.StartBackgroundColor(Color.Brown));
							}
							break;

						case TerrainTypes.Water:
							if (Colors.CurrentBackground != Color.LightBlue)
							{
								Console.Write(Ansi.StartBackgroundColor(Color.LightBlue));
							}
							break;

						case TerrainTypes.Grass:
							if (Colors.CurrentBackground != Color.LightGreen)
							{
								Console.Write(Ansi.StartBackgroundColor(Color.LightGreen));
							}
							break;

						case TerrainTypes.Field:
							if (Colors.CurrentBackground != Color.Orange)
							{
								Console.Write(Ansi.StartBackgroundColor(Color.Orange));
							}
							break;

						case TerrainTypes.Road:
							if (Colors.CurrentBackground != Color.Gray)
							{
								Console.Write(Ansi.StartBackgroundColor(Color.Gray));
							}
							break;

						default:
							throw new InvalidOperationException("Error drawing board! Unexpected terrain type! " + cellTerrain);
					}

					Console.Write(" ");
				}

				Console.WriteLine(Ansi.StartBackgroundColor(Colors.SavedBackground));
			}

			Colors.LoadSavedColors();
		}
	}
}
