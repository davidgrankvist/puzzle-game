﻿namespace puzzle_game
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
			var render = entity.GetComponent<Render>();
			var body = entity.GetComponent<Body>();

			if (render == null || body == null)
			{
				return;
			}

			if (body.Shape is Rectangle)
			{
				var rect = (Rectangle)body.Shape;
				Rl.DrawRectangle(body.X, body.Y, rect.Width, rect.Height, render.FillColor);
			}
		}
	}
}
