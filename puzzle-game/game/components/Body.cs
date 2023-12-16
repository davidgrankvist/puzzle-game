namespace puzzle_game
{
	public class Body : IComponent
	{
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Angle { get; set; }


        public Body(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			Angle = 0;
		}
	 }
}
