namespace puzzle_game
{
	public class KeyboardControlSystem : ISystem
	{
		List<Entity> Entities;

		public KeyboardControlSystem(List<Entity> entities)
		{
			this.Entities = entities;
		}

		public void Load()
		{
		}

		public void Update()
		{
			var player = Entities.Find((entity) => entity.HasComponent<KeyboardControl>());
			if (player == null)
			{
				return;
			}

			UpdatePlayer(player);
		}

		void UpdatePlayer(Entity player) {
			var body = player.GetComponent<Body>();

			if (body == null)
			{
				return;
			}

			var speed = 5;

			if (Rl.IsKeyDown(R.KeyboardKey.KEY_W))
			{
				body.Y -= speed;
			}
			if (Rl.IsKeyDown(R.KeyboardKey.KEY_A))
			{
				body.X -= speed;
			}
			if (Rl.IsKeyDown(R.KeyboardKey.KEY_S))
			{
				body.Y += speed;
			}
			if (Rl.IsKeyDown(R.KeyboardKey.KEY_D))
			{
				body.X += speed;
			}
		}
	}
}
