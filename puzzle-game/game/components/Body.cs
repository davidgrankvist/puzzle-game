using System.Numerics;

namespace puzzle_game
{
	public class Body : IComponent
	{
        public int X { get; set; }
        public int Y { get; set; }
		public IShape Shape { get; set; }

        public float Angle { get; set; }
		public Vector2 Origin { get; set; }


        public Body(int x, int y, IShape shape)
		{
			X = x;
			Y = y;
			Shape = shape;
			Angle = 0;
			Origin = new Vector2(0, 0);
		}

		public Body(int x, int y, IShape shape, float angle, Vector2 origin)
		{
			X = x;
			Y = y;
			Shape = shape;
			Angle = angle;
			Origin = origin;
		}
	 }
}
