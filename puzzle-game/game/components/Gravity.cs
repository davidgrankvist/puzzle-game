namespace puzzle_game.Game.Components
{
    public class Gravity : IComponent
    {
        static float DEFAULT_GRAVITY = 1;

        public float Ay { get; set; }

        public Gravity()
        {
            Ay = DEFAULT_GRAVITY;
        }

        public Gravity(int gravity)
        {
            Ay = gravity;
        }
    }
}
