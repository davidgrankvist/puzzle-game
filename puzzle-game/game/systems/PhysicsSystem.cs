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
				// part 1 - move along orbit
				// get current angle relative to center
				// add angular velocity
				// calculate the new position

				// part 2 - rotate body so that it faces the origin
				// probably need to know the corner positions
				// if we rotate all of the corner positions in part 1, then that should solve part 2

				// part 3 - collision detection
				// part 3.1 - when doing the orbit, don't rotate through the player
				// part 3.2 - when done rotating, player needs to check for rotated rectangles
			}
		}
	}
}
