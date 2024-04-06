using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;

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

                // accelerates way too fast, so cap it for now
                if (physicsBody.Vy < Constants.PLAYER_SPEED * 2)
                {
                    physicsBody.Vy += gravity.Ay;
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
