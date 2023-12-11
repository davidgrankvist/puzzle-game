namespace puzzle_game
{
	public class Gravity : IComponent
	{
		static int DEFAULT_GRAVITY = 1;

		public int Ay { get; set; }

		public Gravity() {
			this.Ay = DEFAULT_GRAVITY;
		}

		public Gravity(int gravity)
		{
			this.Ay = gravity;
		}
	}
}
