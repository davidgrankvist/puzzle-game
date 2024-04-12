using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes.MazeGeneration
{
	internal class RandomMazeGenerator : IMazeGenerator
	{
		private readonly Random random;
        public RandomMazeGenerator()
        {
            random = new Random();
        }

        public RandomMazeGenerator(int seed)
        {
			random = new Random(seed);
        }

        public MazeGraph Generate(int rows, int cols)
		{
			var mazeGraph = new MazeGraph(rows, cols);
			mazeGraph.InitializeEdges();

			foreach (var edge in mazeGraph.GetAllEdges())
			{
				var isBlocked = random.Next(2) == 0;
				mazeGraph.UpdateBothEdges(edge.To, edge.From, isBlocked);
			}
			return mazeGraph;
		}
	}
}
