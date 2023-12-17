using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace puzzle_game
{
	public class PhysicsSystem : ISystem
	{
		List<Entity> Entities;
		CollisionDetectionService CollisionDetectionService;

		public PhysicsSystem(List<Entity> entities)
		{
			Entities = entities;
			CollisionDetectionService = new CollisionDetectionService();
		}
		public void Load()
		{
		}

		public void Update()
		{
			var physicsEntities = Entities
				.Where(IsPhysicsEntity)
				.ToList();

			if (physicsEntities == null || physicsEntities.Count == 0)
			{
				return;
			}

			ApplyOrbits(physicsEntities);
			ApplyGravity(physicsEntities);
			CollisionDetectionService.CheckAndApplyCollisions(physicsEntities);
			MovePlayer(physicsEntities);
		}

		bool IsPhysicsEntity(Entity entity)
		{
			return entity.HasComponent<PhysicsBody>()
				&& entity.HasComponent<Body>();
		}

		void ApplyGravity(List<Entity> physicsEntities)
		{
			var entitiesWithGravity = physicsEntities.Where((entity) => entity.HasComponent<Gravity>()).ToList();

			foreach (var entity in entitiesWithGravity)
			{
				var physicsBody = entity.GetComponentUnsafe<PhysicsBody>();
				var gravity = entity.GetComponentUnsafe<Gravity>();

				// accelerates way too fast, so cap it for now
				if (physicsBody.Vy < Constants.PLAYER_SPEED * 2)
				{
					physicsBody.Vy += gravity.Ay;
				}
			}
		}

		void MovePlayer(List<Entity> physicsEntities)
		{
			var player = physicsEntities.Find((entity) => entity.HasComponent<ControlledMovement>());

			if (player == null)
			{
				return;
			}

			var playerBody = player.GetComponentUnsafe<Body>();
			var playerPhysicsBody = player.GetComponentUnsafe<PhysicsBody>();

			playerBody.X += playerPhysicsBody.Vx;
			playerBody.Y += playerPhysicsBody.Vy;
		}

		void ApplyOrbits(List<Entity> physicsEntities)
		{
			var entitiesWithOrbit = physicsEntities.Where((entity) => entity.HasComponent<Orbit>());

			foreach (var entity in entitiesWithOrbit)
			{
				var body = entity.GetComponentUnsafe<Body>();
				var orbit = entity.GetComponentUnsafe<Orbit>();

				if (orbit.Sign == 0)
				{
					continue;
				}

				var center = new Vector2(orbit.CenterX, orbit.CenterY);
				var topLeft = new Vector2(body.X, body.Y);
				var orbitAngleDelta = (float)(orbit.Sign) * orbit.AngularVelocity;

				// calculate next top left
				// translate to check orbit around origin, rotate, translate back
				var topLeftTranslated = new Vector2(topLeft.X - center.X, topLeft.Y - center.Y);
				var topLeftTranslatedRotated = Rm.Vector2Rotate(topLeftTranslated, orbitAngleDelta);
				var topLeftXNext = topLeftTranslatedRotated.X + center.X;
				var topLeftYNext = topLeftTranslatedRotated.Y + center.Y;

				// also calculate top right in order to determine how the rectangle is rotated
				var topRight = new Vector2(
					topLeft.X + body.Width * MathF.Cos(body.Angle),
					topLeft.Y + body.Width * MathF.Sin(body.Angle)
				);
				var topRightTranslated = new Vector2(topRight.X - center.X, topRight.Y - center.Y);
				var topRightTranslatedRotated = Rm.Vector2Rotate(topRightTranslated, orbitAngleDelta);
				var topRightXNext = topRightTranslatedRotated.X + center.X;
				var topRightYNext = topRightTranslatedRotated.Y + center.Y;
				// translate top left of rectangle to origin to determine the angle
				var angleNext = MathF.Acos((topRightXNext - topLeftXNext) / body.Width);

				// update body
				body.X = topLeftXNext;
				body.Y = topLeftYNext;
				body.Angle = angleNext;
			}
		}
	}
}
