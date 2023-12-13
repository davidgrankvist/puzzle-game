using System.Numerics;

namespace puzzle_game
{
	public class Orbit : IComponent
	{
		public Vector2 Center { get; set; }
        public float AngularVelocity { get; set; }
        public Direction Sign { get; set; }

        public Orbit(Vector2 origin, float angularVelocity, Direction sign)
        {
            Center = origin;
            AngularVelocity = angularVelocity;
            Sign = sign;
        }

        public enum Direction
        {
            Left = -1,
            Stationary = 0,
            Right = 1,
        }
    }
}
