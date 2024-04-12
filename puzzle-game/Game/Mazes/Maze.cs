using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Mazes
{
    public class Maze
    {
        public Maze(IEnumerable<MazeBlock> blocks)
        {
            Blocks = blocks;
        }
        public IEnumerable<MazeBlock> Blocks { get; }
    }
}
