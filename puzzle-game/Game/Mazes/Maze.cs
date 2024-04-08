using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes
{
    public class Maze
    {
		public IEnumerable<MazeBlock> Blocks { get; set; } = new List<MazeBlock>();    
    }
}
