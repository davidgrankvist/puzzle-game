using Raylib_cs;

namespace puzzle_game.Game.Components
{
    public class Render : IComponent
    {
        public Color FillColor { get; set; }

        public Render(Color color)
        {
            FillColor = color;
        }
    }
}
