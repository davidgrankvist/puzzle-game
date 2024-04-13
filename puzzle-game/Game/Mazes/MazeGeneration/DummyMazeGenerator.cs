using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes.MazeGeneration
{
	internal class DummyMazeGenerator : IMazeGenerator
	{
		public MazeGraph Generate(int rows, int cols)
		{
			var mazeGraph = new MazeGraph(rows, cols);
			mazeGraph.InitializeEdges();
			return mazeGraph;
		}
	}
}
