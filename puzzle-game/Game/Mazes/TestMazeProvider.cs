using puzzle_game.Game.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes
{
	public class TestMazeProvider : IMazeProvider
	{
		public Maze GetMaze()
		{
			var borderTop = new MazeBlock(0, -1, Constants.WINDOW_WIDTH, 1)
			{
				ShouldRender = false,
			};
			var borderLeft = new MazeBlock(-1, 0, 1, Constants.WINDOW_HEIGHT)
			{
				ShouldRender = false,
			};
			var borderRight = new MazeBlock(Constants.WINDOW_WIDTH, 0, 1, Constants.WINDOW_HEIGHT)
			{
				ShouldRender = false,
			};
			var ground = new MazeBlock(0, Constants.WINDOW_HEIGHT - 50, Constants.WINDOW_WIDTH, 50);
			var obstacle = new MazeBlock(400, Constants.WINDOW_HEIGHT - 100, 50, 50);
			var flyingObstacle = new MazeBlock(450, Constants.WINDOW_HEIGHT - 200, 80, 50);


			var maze = new Maze()
			{
				Blocks = new List<MazeBlock>()
				{
					borderTop,
					borderLeft,
					borderRight,
					ground,
					obstacle,
					flyingObstacle,
				}
			};
			return maze;
		}
	}
}
