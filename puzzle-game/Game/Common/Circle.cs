using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game.Game.Common
{
	public class Circle : IShape
	{
        public float Radius { get; set; }
        
        public Circle(float radius)
        {
            Radius = radius;
        }
    }
}
