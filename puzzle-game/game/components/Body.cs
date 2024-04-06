using puzzle_game.Game.Common;

namespace puzzle_game.Game.Components
{
    public class Body : IComponent
    {
        public int X { get; set; }
        public int Y { get; set; }

        public IShape Shape { get; set; }

        public Body(int x, int y, IShape shape)
        {
            X = x;
            Y = y;
            Shape = shape;
        }
    }
}
