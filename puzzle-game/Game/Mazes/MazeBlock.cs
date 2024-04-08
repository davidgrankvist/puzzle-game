using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes
{
	public struct MazeBlock
	{

		public float X;

		public float Y;

		public int Width;

		public int Height;

		public MazeBlock(float x, float y, int width, int height)
        {
			X = x;
			Y = y;
			Width = width;
			Height = height;
        }

	}
}
