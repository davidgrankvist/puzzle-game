namespace puzzle_game.Game.Components
{
    public class Gravity : IComponent
    {
        static int DEFAULT_GRAVITY = 1;

        public int Ay { get; set; }

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
