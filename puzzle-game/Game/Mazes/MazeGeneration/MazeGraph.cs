using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes.MazeGeneration
{
	public class MazeGraph
	{
		public readonly int NumNodes;
		public readonly int Rows;
		public readonly int Cols;
		private readonly List<MazeEdge>[] Edges;

        public MazeGraph(int rows, int cols)
        {
			Rows = rows;
			Cols = cols;
            NumNodes = rows * cols;
			Edges = new List<MazeEdge>[NumNodes];
        }

        public class MazeEdge
		{
			public readonly int From;
			public readonly int To;
			public bool IsBlocked;

			public MazeEdge(int from, int to)
            {
                From = from;
				To = to;
				IsBlocked = true;
            }
		}

		public void InitializeEdges()
		{
            for (int i = 0; i < NumNodes; i++)
			{
				int row = ToRow(i);
				int col = ToCol(i);

				var neighborEdges = new List<MazeEdge>();

				if (row > 0)
				{
					var up = ToIndex(row - 1, col);
					neighborEdges.Add(new MazeEdge(i, up));
				}

				if (row < Rows - 1)
				{
					var down = ToIndex(row + 1, col);
					neighborEdges.Add(new MazeEdge(i, down));
				}

				if (col > 0)
				{
					var left = ToIndex(row, col - 1);
					neighborEdges.Add(new MazeEdge(i, left));
				}

				if (col < Cols - 1)
				{
					var right = ToIndex(row, col + 1);
					neighborEdges.Add(new MazeEdge(i, right));
				}

				Edges[i] = neighborEdges;
            }
		}

		public int ToRow(int index)
		{
			return index / Cols;
		}

		public int ToCol(int index)
		{
			return index % Cols;
		}

		public int ToIndex(int row, int col)
		{
			return row * Cols + col;
		}

		public void UpdateBothEdges(int i, int j, bool isBlocked)
		{
			var ijEdge = Edges[i].Find(edge => edge.To == j);
			var jiEdge = Edges[j].Find(edge => edge.To == i);

			if (ijEdge != null)
			{
				ijEdge.IsBlocked = isBlocked;
			}

			if (jiEdge != null)
			{
				jiEdge.IsBlocked = isBlocked;
			}
		}

		public List<MazeEdge> GetEdges(int i)
		{
			return Edges[i];
		}

		public IEnumerable<MazeEdge> GetAllEdges()
		{
			for (int i = 0; i < NumNodes; i++) 
			{ 
				foreach (var edge in Edges[i]) 
				{ 
					yield return edge;
				}
			}
		}

	}

}
