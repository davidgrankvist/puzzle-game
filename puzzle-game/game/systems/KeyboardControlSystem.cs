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
			var body = player.GetComponent<PhysicsBody>();

			if (body == null)
			{
				return;
			}

			body.Vx = 0;
			body.Vy = 0;

			if (Rl.IsKeyDown(R.KeyboardKey.KEY_W))
			{
				body.Vy = -Constants.PLAYER_SPEED;
			}
			if (Rl.IsKeyDown(R.KeyboardKey.KEY_A))
			{
				body.Vx = -Constants.PLAYER_SPEED;
			}
			if (Rl.IsKeyDown(R.KeyboardKey.KEY_S))
			{
				body.Vy = Constants.PLAYER_SPEED;
			}
			if (Rl.IsKeyDown(R.KeyboardKey.KEY_D))
			{
				body.Vx = Constants.PLAYER_SPEED;
			}
		}
	}
}
