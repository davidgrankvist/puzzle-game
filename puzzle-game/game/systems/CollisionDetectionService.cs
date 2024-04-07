using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;

namespace puzzle_game.Game.Systems
{
    public class CollisionDetectionService
    {
        // These are for avoiding allocating Raylib structs over and over again while running collision detection.
        // Should probably be in the components instead..
        Raylib_cs.Rectangle playerRlRectNextX;
        Raylib_cs.Rectangle playerRlRectNextY;
        Raylib_cs.Rectangle entityRlRect;

        public CollisionDetectionService()
        {
            playerRlRectNextX = new Raylib_cs.Rectangle();
            playerRlRectNextY = new Raylib_cs.Rectangle();
            entityRlRect = new Raylib_cs.Rectangle();
        }

        public void CheckAndApplyCollisions(List<Entity> entities)
        {
            var physicsEntities = entities.Where(entity => entity.HasComponent<Body>() && entity.HasComponent<PhysicsBody>()).ToList();
            var player = physicsEntities.Find((entity) => entity.HasComponent<KeyboardControl>());

            if (player == null)
            {
                return;
            }

            var playerPhysics = player.GetComponentUnsafe<PhysicsBody>();
            var playerBody = player.GetComponentUnsafe<Body>();
            if (playerBody.Shape is not Rectangle)
            {
                return;
            }

            var playerRect = (Rectangle)playerBody.Shape;

            playerRlRectNextX.X = playerBody.X + playerPhysics.Vx;
            playerRlRectNextX.Y = playerBody.Y;
            playerRlRectNextX.Width = playerRect.Width;
            playerRlRectNextX.Height = playerRect.Height;

            playerRlRectNextY.X = playerBody.X;
            playerRlRectNextY.Y = playerBody.Y + playerPhysics.Vy;
            playerRlRectNextY.Width = playerRect.Width;
            playerRlRectNextY.Height = playerRect.Height;

            foreach (var entity in physicsEntities)
            {
                if (entity == player)
                {
                    continue;
                }
                var entityBody = entity.GetComponentUnsafe<Body>();
                if (entityBody.Shape is not Rectangle)
                {
                    continue;
                }
                var entityRect = (Rectangle)entityBody.Shape;
                entityRlRect.X = entityBody.X;
                entityRlRect.Y = entityBody.Y;
                entityRlRect.Width = entityRect.Width;
                entityRlRect.Height = entityRect.Height;

                bool willCollideX = Raylib_cs.Raylib.CheckCollisionRecs(playerRlRectNextX, entityRlRect);
                bool willCollideY = Raylib_cs.Raylib.CheckCollisionRecs(playerRlRectNextY, entityRlRect);

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
