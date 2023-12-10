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

		public static Entity CreatePlayer()
		{
			var player = new Entity();

			const int x = Constants.WINDOW_WIDTH / 2;
			const int y = Constants.WINDOW_HEIGHT / 2;

			const int width = 10;
			const int height = 15;

			player.AddComponent(new Body(x, y, new Rectangle(width, height)));
			player.AddComponent(new KeyboardControl());
			player.AddComponent(new Render(R.Color.BLUE));

			return player;
		}
	}
}
