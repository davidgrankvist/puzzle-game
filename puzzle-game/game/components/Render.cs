namespace puzzle_game
{
	public class Render : IComponent
	{
        public R.Color FillColor { get; set; }

		public Render(R.Color color)
		{
			FillColor = color;
		}
    }
}
