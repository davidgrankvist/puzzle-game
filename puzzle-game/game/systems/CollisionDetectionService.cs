
namespace puzzle_game
{
	public class CollisionDetectionService
	{
		// These are for avoiding allocating Raylib structs over and over again while running collision detection.
		// Should probably be in the components instead..
		R.Rectangle playerRlRectNextX;
		R.Rectangle playerRlRectNextY;
		R.Rectangle entityRlRect;

		public CollisionDetectionService()
		{
			playerRlRectNextX = new R.Rectangle();
			playerRlRectNextY = new R.Rectangle();
			entityRlRect = new R.Rectangle();
		}

		public void CheckAndApplyCollisions(List<Entity> physicsEntities)
		{
			var player = physicsEntities.Find((entity) => entity.HasComponent<ControlledMovement>());

			if (player == null)
			{
				return;
			}

			var playerPhysics = player.GetComponentUnsafe<PhysicsBody>();
			var playerBody = player.GetComponentUnsafe<Body>();

			playerRlRectNextX.X = playerBody.X + playerPhysics.Vx;
			playerRlRectNextX.Y = playerBody.Y;
			playerRlRectNextX.Width = playerBody.Width;
			playerRlRectNextX.Height = playerBody.Height;

			playerRlRectNextY.X = playerBody.X;
			playerRlRectNextY.Y = playerBody.Y + playerPhysics.Vy;
			playerRlRectNextY.Width = playerBody.Width;
			playerRlRectNextY.Height = playerBody.Height;

			foreach (var entity in physicsEntities)
			{
				if (entity == player)
				{
					continue;
				}
				var entityBody = entity.GetComponentUnsafe<Body>();

				entityRlRect.X = entityBody.X;
				entityRlRect.Y = entityBody.Y;
				entityRlRect.Width = entityBody.Width;
				entityRlRect.Height = entityBody.Height;

				bool willCollideX = Rl.CheckCollisionRecs(playerRlRectNextX, entityRlRect);
				bool willCollideY = Rl.CheckCollisionRecs(playerRlRectNextY, entityRlRect);

				if (willCollideX)
				{
					playerPhysics.Vx = 0;
				}
				if (willCollideY)
				{
					playerPhysics.Vy = 0;
				}
			}
		}
	}
}
