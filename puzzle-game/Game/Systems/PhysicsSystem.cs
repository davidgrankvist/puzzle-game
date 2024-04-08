using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;

namespace puzzle_game.Game.Systems
{
    public class PhysicsSystem : ISystem
    {
        private List<Entity> entities;
        private CollisionDetectionService collisionDetectionService;

        public PhysicsSystem(List<Entity> entities)
        {
            this.entities = entities;
            collisionDetectionService = new CollisionDetectionService();
        }
        public void Load()
        {
        }

        public void Update()
        {
            ApplyGravity();
            collisionDetectionService.CheckAndApplyCollisions(entities);
            MoveEntities();
        }

        private void ApplyGravity()
        {
            var entitiesWithGravity = entities.Where((entity) => entity.HasComponent<Gravity>()).ToList();

            foreach (var entity in entitiesWithGravity)
            {
                var physicsBody = entity.GetComponentUnsafe<PhysicsBody>();
                var gravity = entity.GetComponentUnsafe<Gravity>();

				var cameraEntity = entities.Find(entity => entity.HasComponent<Camera>());
				if (cameraEntity == null)
				{
					return;
				}
				var camera = cameraEntity.GetComponentUnsafe<Camera>();
				var angle = -camera.RotationRadians;

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

        private void MoveEntities()
        {
            var player = entities.Find((entity) => entity.HasComponent<KeyboardControl>());

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
