namespace puzzle_game
{
	class RenderSystem : ISystem
	{
		List<Entity> Entities;

		public RenderSystem(List<Entity> entities) {
			Entities = entities;
		}

		public void Load()
		{
		}

		public void Update()
		{
			foreach(var entity in Entities)
			{
				renderEntity(entity);
			}
		}

		void renderEntity(Entity entity)
		{
			if (!entity.HasComponent<Render>() || !entity.HasComponent<Body>())
			{
				return;
			}
			var render = entity.GetComponentUnsafe<Render>();
			var body = entity.GetComponentUnsafe<Body>();

			Rl.DrawRectangle(body.X, body.Y, body.Width, body.Height, render.FillColor);
		}
	}
}
