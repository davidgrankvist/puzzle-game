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


			// jump
			// could be mid-air with Vy = 0, but that's hard to time with a jump keypress
			if (body.Vy == 0 && Rl.IsKeyPressed(R.KeyboardKey.KEY_W))
			{
				body.Vy = -Constants.PLAYER_SPEED * 3;
			}

			// move left/right
			body.Vx = 0;
			if (Rl.IsKeyDown(R.KeyboardKey.KEY_A))
			{
				body.Vx = -Constants.PLAYER_SPEED;
			}
			if (Rl.IsKeyDown(R.KeyboardKey.KEY_D))
			{
				body.Vx = Constants.PLAYER_SPEED;
			}
		}
	}
}
