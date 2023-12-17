using System.Numerics;

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

			// draw orbit center for debugging
			Rl.DrawCircle(Constants.CENTER_X, Constants.CENTER_Y, 5, R.Color.RED);
		}

		void renderEntity(Entity entity)
		{
			if (!entity.HasComponent<Render>() || !entity.HasComponent<Body>())
			{
				return;
			}
			var render = entity.GetComponentUnsafe<Render>();
			var body = entity.GetComponentUnsafe<Body>();

			// draw lines for now to make the debug corners easier to see
			Rl.DrawRectangleLines((int)body.X, (int)body.Y, body.Width, body.Height, render.FillColor);

			// debug rendering of corners to see that orbits work
			if (entity.HasComponent<Orbit>())
			{
				/*
				 * Bugs:
				 *	- the rotation doesn't go past 180 degrees. Physics system bug?
				 *	- rectangle corners become misplaced when rotating. math bug below?
				 *		- maybe just bottom right (green)? think thonk hmmmm
				 *  - sometimes the corners disappear. what?
				 * 
				 * Rotate point around origin:
				 *	X = x*cos(t) - y*sin(t)
				 *	Y = x*sin(t) + y*cos(t)
				 * Source: https://gamedev.stackexchange.com/questions/86755/how-to-calculate-corner-positions-marks-of-a-rotated-tilted-rectangle
				 */
				var size = 20;
				var topLeft = new Vector2(
					body.X,
					body.Y
				);
				var topRight = new Vector2(
					body.X + body.Width * MathF.Cos(body.Angle),
					body.Y + body.Width * MathF.Sin(body.Angle)
				);
				var bottomRight = new Vector2(
					body.X + body.Width * MathF.Cos(body.Angle) - body.Height * MathF.Sin(body.Angle),
					body.Y + body.Height * MathF.Sin(body.Angle) + body.Height * MathF.Cos(body.Angle)
				);
				var bottomLeft = new Vector2(
					body.X - body.Height * MathF.Sin(body.Angle),
					body.Y + body.Height * MathF.Cos(body.Angle)
				);
				Rl.DrawRectangle((int)topLeft.X - size / 2, (int)topLeft.Y - size / 2, size, size, R.Color.RED);
				Rl.DrawRectangle((int)topRight.X - size / 2, (int)topRight.Y - size / 2, size, size, R.Color.VIOLET);
				Rl.DrawRectangle((int)bottomRight.X - size / 2, (int)bottomRight.Y - size / 2, size, size, R.Color.GREEN);
				Rl.DrawRectangle((int)bottomLeft.X - size / 2, (int)bottomLeft.Y - size / 2, size, size, R.Color.BLUE);
			}
		}
	}
}
