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
				var position = new Vector2(body.X, body.Y);

				// translate, rotate, translate back
				var tpos = new Vector2(position.X - center.X, position.Y - center.Y);
				var angleDelta = (float)(orbit.Sign) * orbit.AngularVelocity;
				var vrot = Rm.Vector2Rotate(tpos, angleDelta);
				vrot.X += center.X;
				vrot.Y += center.Y;

				body.X = vrot.X;
				body.Y = vrot.Y;
			}
		}
	}
}
