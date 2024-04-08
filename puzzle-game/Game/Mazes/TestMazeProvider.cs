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
			var borderTop = new MazeBlock(0, 0, Constants.WINDOW_WIDTH, 50);
			var borderLeft = new MazeBlock(0, 0, 50, Constants.WINDOW_HEIGHT);
			var borderRight = new MazeBlock(Constants.WINDOW_WIDTH - 50, 0, 50, Constants.WINDOW_HEIGHT);
			var ground = new MazeBlock(0, Constants.WINDOW_HEIGHT - 50, Constants.WINDOW_WIDTH, 50);

			var topObstacle = new MazeBlock(300, 50, 50, 50);
			var leftObstacle = new MazeBlock(50, 100, 100, 50);
			var rightObstacle = new MazeBlock(Constants.WINDOW_WIDTH - 250, 250, 200, 50);
			var groundObstacle1 = new MazeBlock(400, Constants.WINDOW_HEIGHT - 100, 50, 50);
			var groundObstacle2 = new MazeBlock(600, Constants.WINDOW_HEIGHT - 100, 50, 50);

			var flyingObstacle1 = new MazeBlock(450, Constants.WINDOW_HEIGHT - 200, 80, 50);
			var flyingObstacle2 = new MazeBlock(500, Constants.WINDOW_HEIGHT - 300, 80, 50);
			var flyingObstacle3 = new MazeBlock(600, Constants.WINDOW_HEIGHT - 400, 80, 50);
			var flyingObstacle4 = new MazeBlock(200, Constants.WINDOW_HEIGHT - 400, 50, 100);

			var maze = new Maze()
			{
				Blocks = new List<MazeBlock>()
				{
					borderTop,
					borderLeft,
					borderRight,
					ground,
					topObstacle,
					leftObstacle,
					rightObstacle,
					groundObstacle1,
					groundObstacle2,
					flyingObstacle1,
					flyingObstacle2,
					flyingObstacle3,
					flyingObstacle4,
				}
			};
			return maze;
		}
	}
}
