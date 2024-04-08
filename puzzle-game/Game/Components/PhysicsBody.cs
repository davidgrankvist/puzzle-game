namespace puzzle_game.Game.Components
{
    public class PhysicsBody : IComponent
    {
        public float Vx { get; set; }
        public float Vy { get; set; }

        public PhysicsBody()
        {
            Vx = 0;
            Vy = 0;
        }

        public PhysicsBody(float vx, float vy)
        {
            Vx = vx;
            Vy = vy;
        }
    }
}
