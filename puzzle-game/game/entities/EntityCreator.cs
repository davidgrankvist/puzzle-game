namespace puzzle_game
{
	public class EntityCreator
	{
		public static Entity CreateGround()
		{
			var ground = new Entity();

			const int height = 50;
			const int y = Constants.WINDOW_HEIGHT - height;

			ground.AddComponent(new Body(0, y, new Rectangle(Constants.WINDOW_WIDTH, height)));
			ground.AddComponent(new Render(R.Color.BLACK));

			return ground;
		}
	}
}
