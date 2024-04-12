using puzzle_game.Game;
using puzzle_game.Game.Mazes.MazeGeneration;

namespace puzzle_game
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var game = new Game.Game();
			game.Run();
		}
	}
}