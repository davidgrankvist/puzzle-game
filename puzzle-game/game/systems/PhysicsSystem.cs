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

			CollisionDetectionService.CheckAndApplyCollisions(physicsEntities);
			MoveEntities(physicsEntities);
		}

		bool IsPhysicsEntity(Entity entity)
		{
			return entity.HasComponent<PhysicsBody>()
				&& entity.HasComponent<Body>();
		}


		void MoveEntities(List<Entity> physicsEntities)
		{
			var player = physicsEntities.Find((entity) => entity.HasComponent<KeyboardControl>());

			if (player == null)
			{
				return;
			}

			var playerBody = player.GetComponentUnsafe<Body>();
			var playerPhysicsBody = player.GetComponentUnsafe<PhysicsBody>();

			playerBody.X += playerPhysicsBody.Vx;
			playerBody.Y += playerPhysicsBody.Vy;
		}
	}
}
