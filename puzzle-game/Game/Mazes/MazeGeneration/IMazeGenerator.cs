using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes.MazeGeneration
{
	internal interface IMazeGenerator
	{
		public MazeGraph Generate(int rows, int cols);
	}
}
