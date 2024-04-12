using puzzle_game.Game.Common;
using puzzle_game.Game.Mazes.MazeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes
{
	internal class GeneratedMazeProvider : IMazeProvider
	{
		private readonly IMazeGenerator mazeGenerator;
		private readonly int Rows;
		private readonly int Cols;
		private Maze? maze;

		private static readonly int BLOCK_SIZE = 50;
		private static readonly int WALL_SIZE = 5;

		public GeneratedMazeProvider(IMazeGenerator mazeGenerator, int rows, int cols)
        {
            this.mazeGenerator = mazeGenerator;
			Rows = rows; 
			Cols = cols;
        }

        public GeneratedMazeProvider(IMazeGenerator mazeGenerator) 
			: this(mazeGenerator, GameConstants.MAZE_ROWS, GameConstants.MAZE_COLS) 
		{
		}

        public Maze GetMaze()
		{
			if (maze == null)
			{
				var mazeGraph = mazeGenerator.Generate(Rows, Cols);
				maze = ToMaze(mazeGraph);
			}
			return maze;
		}

		private Maze ToMaze(MazeGraph mazeGraph)
		{
			/*
			 * Create a grid where the graph nodes are empty cells
			 * and the blocked edges are thin walls between them.
			 */
			var mazeBlocks = new List<MazeBlock>();

			var hasVisited = new HashSet<(int From, int To)>();
			foreach (var edge in mazeGraph.GetAllEdges())
			{
				if (!edge.IsBlocked || hasVisited.Contains((edge.From, edge.To)))
				{
					continue;
				}
				hasVisited.Add((edge.From, edge.To));
				hasVisited.Add((edge.To, edge.From));

				var fromRow = mazeGraph.ToRow(edge.From);
				var fromCol = mazeGraph.ToCol(edge.From);
				var topLeft = ToPosition(fromRow, fromCol);

				var toRow = mazeGraph.ToRow(edge.To);
				var toCol = mazeGraph.ToCol(edge.To);

				MazeBlock block;

				if (fromRow < toRow) // neighbor is above
				{
					// wall from top left to top right
					block = new MazeBlock(topLeft.X, topLeft.Y, BLOCK_SIZE, WALL_SIZE);

				} else if (fromRow > toRow) // neighbor is below
				{
					// wall from bottom left to bottom right
					block = new MazeBlock(topLeft.X, topLeft.Y + BLOCK_SIZE, BLOCK_SIZE, WALL_SIZE);
				}
				else if (fromCol > toRow) // neighbor is to the left
				{
					// wall from top left to bottom left
					block = new MazeBlock(topLeft.X, topLeft.Y, WALL_SIZE, BLOCK_SIZE);
				}
				else if (fromCol < toCol) // neighbor is to the right
				{
					// wall from top right to bottom right
					block = new MazeBlock(topLeft.X + BLOCK_SIZE, topLeft.Y + BLOCK_SIZE, WALL_SIZE, BLOCK_SIZE);
				}
				else
				{
					throw new InvalidOperationException("Unexpected graph edge. Please verify that the graph is a grid.");
				}

				mazeBlocks.Add(block);
			}

			return new Maze(mazeBlocks);
		}

		private Vector2 ToPosition(int row, int col)
		{
			var offsetX = GameConstants.CENTER_X - Cols * BLOCK_SIZE / 2;
			var offsetY = GameConstants.CENTER_Y - Rows * BLOCK_SIZE / 2;

			var x = offsetX + BLOCK_SIZE * row; 
			var y = offsetY + BLOCK_SIZE * col;

			return new Vector2(x, y);
		}
	}
}
