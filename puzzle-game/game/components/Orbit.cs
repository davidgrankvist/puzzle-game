using System.Numerics;

namespace puzzle_game
{
	public class Orbit : IComponent
	{
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float AngularVelocity { get; set; }
        public Direction Sign { get; set; }

        public Orbit(float centerX, float centerY, float angularVelocity, Direction sign)
        {
            CenterX = centerX;
            CenterY = centerY;
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
