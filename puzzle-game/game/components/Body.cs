using puzzle_game.Game.Common;

namespace puzzle_game.Game.Components
{
    public class Body : IComponent
    {
        public float X { get; set; }
        public float Y { get; set; }

        public IShape Shape { get; set; }

        public Body(float x, float y, IShape shape)
        {
            X = x;
            Y = y;
            Shape = shape;
        }
    }
}
