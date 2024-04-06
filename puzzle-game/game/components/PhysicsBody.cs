namespace puzzle_game.Game.Components
{
    public class PhysicsBody : IComponent
    {
        public int Vx { get; set; }
        public int Vy { get; set; }

        public PhysicsBody()
        {
            Vx = 0;
            Vy = 0;
        }

        public PhysicsBody(int vx, int vy)
        {
            Vx = vx;
            Vy = vy;
        }
    }
}
