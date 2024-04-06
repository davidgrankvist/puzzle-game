using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;
using Raylib_cs;

namespace puzzle_game.Game.Systems
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
            MoveEntities(physicsEntities);
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

				var cameraEntity = Entities.Find(entity => entity.HasComponent<Camera>());
				if (cameraEntity == null)
				{
					return;
				}
				var camera = cameraEntity.GetComponentUnsafe<Camera>();
				var angle = -camera.Rotation * MathF.PI / 180f;

                var vx = physicsBody.Vx;
                var vy = physicsBody.Vy;

				var gravityDx = -MathF.Sin(angle);
				var gravityDy =  MathF.Cos(angle);
                var ax = gravityDx * gravity.Ay;
                var ay = gravityDy * gravity.Ay;

                // update velocity, but also cap it as it accelerates very quickly
				var totalVelocityTowardsGravity = vx * gravityDx + vy * gravityDy;
				if (totalVelocityTowardsGravity < Constants.PLAYER_SPEED * 2)
                {
                    physicsBody.Vx += ax;
                    physicsBody.Vy += ay;
                }
            }
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
