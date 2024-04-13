using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes.MazeGeneration
{
	internal class DfsMazeGenerator : IMazeGenerator
	{
		private Random random;

        public DfsMazeGenerator()
        {
            random = new Random();
        }

        public DfsMazeGenerator(int seed)
        {
            random = new Random(seed);
        }

        public MazeGraph Generate(int rows, int cols)
		{
			var mazeGraph = new MazeGraph(rows, cols);
			mazeGraph.InitializeEdges();

			Traverse(mazeGraph);

			return mazeGraph;
		}
		private void Traverse(MazeGraph mazeGraph)
		{
			var isVisited = new bool[mazeGraph.NumNodes];
			var nodesToVisit = new Stack<int>();
			
			var start = random.Next(mazeGraph.NumNodes);
			nodesToVisit.Push(start);

			while (nodesToVisit.Count > 0)
			{
				var current = nodesToVisit.Peek();

				var edges = mazeGraph.GetEdges(current);
				var edgesToUnvisited = edges.Where(edge => !isVisited[edge.To]).ToList();

				if (edgesToUnvisited.Count > 0)
				{
					// unblock the path to a random unvisited neighbor
					var randomNeighbor = random.Next(edgesToUnvisited.Count);
					var edge = edgesToUnvisited[randomNeighbor];
					mazeGraph.UpdateBothEdges(edge.From, edge.To, false);

					// mark the neighbor as visited and go there
					var next = edge.To;
					isVisited[next] = true;
					nodesToVisit.Push(next);
				} else
				{
					// otherwise, we reached a dead end and need to backtrack
					nodesToVisit.Pop();
				}

			}
		}
	}
}
