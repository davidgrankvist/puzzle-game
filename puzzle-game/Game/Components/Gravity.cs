namespace puzzle_game.Game.Components
{
    public class Gravity : IComponent
    {
        public float Ay { get; set; }

        public Gravity(float gravity)
        {
            Ay = gravity;
        }
    }
}
