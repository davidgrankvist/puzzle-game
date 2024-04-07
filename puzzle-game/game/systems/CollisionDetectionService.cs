using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;
using System.Numerics;

namespace puzzle_game.Game.Systems
{
    public class CollisionDetectionService
    {
        // These are for avoiding allocating structs over and over again while running collision detection.
        Vector2 playerPosNextX;
        Vector2 playerPosNextY;
        Raylib_cs.Rectangle entityRlRect;

        public CollisionDetectionService()
        {
            playerPosNextX = new Vector2();
            playerPosNextY = new Vector2();
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
            if (playerBody.Shape is not Circle)
            {
                return;
            }

            var playerCircle = (Circle)playerBody.Shape;

            playerPosNextX.X = playerBody.X + playerPhysics.Vx;
            playerPosNextX.Y = playerBody.Y;
            
            playerPosNextY.X = playerBody.X;
			playerPosNextY.Y = playerBody.Y + playerPhysics.Vy;

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

                bool willCollideX = Raylib_cs.Raylib.CheckCollisionCircleRec(playerPosNextX, playerCircle.Radius, entityRlRect);
                bool willCollideY = Raylib_cs.Raylib.CheckCollisionCircleRec(playerPosNextY, playerCircle.Radius, entityRlRect);

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
