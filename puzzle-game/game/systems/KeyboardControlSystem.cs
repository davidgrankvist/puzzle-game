using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;
using System.Numerics;

namespace puzzle_game.Game.Systems
{
    public class KeyboardControlSystem : ISystem
    {
        List<Entity> Entities;

        public KeyboardControlSystem(List<Entity> entities)
        {
            Entities = entities;
        }

        public void Load()
        {

        }

        public void Update()
        {
            UpdateCamera();
            UpdatePlayer();
        }

        void UpdateCamera()
        {
            var cameraEntity = Entities.Find(entity => entity.HasComponent<Camera>());
            if (cameraEntity == null)
            {
                return;
            }
            var camera = cameraEntity.GetComponentUnsafe<Camera>();

            if (Rl.IsKeyDown(R.KeyboardKey.KEY_LEFT))
            {
                camera.Rotation -= Constants.CAMERA_ROTATION_SPEED;
            }
            if (Rl.IsKeyDown(R.KeyboardKey.KEY_RIGHT))
            {
                camera.Rotation += Constants.CAMERA_ROTATION_SPEED;
            }
        }

        void UpdatePlayer()
        {
            var player = Entities.Find((entity) => entity.HasComponent<KeyboardControl>());
            if (player == null)
            {
                return;
            }

            var body = player.GetComponent<PhysicsBody>();

            if (body == null)
            {
                return;
            }

            // jump
            // could be mid-air with Vy = 0, but that's hard to time with a jump keypress
            if (body.Vy == 0 && Rl.IsKeyPressed(R.KeyboardKey.KEY_W))
            {
                body.Vy = -Constants.PLAYER_JUMP_SPEED;
            }

            // move left/right
            body.Vx = 0;
            if (Rl.IsKeyDown(R.KeyboardKey.KEY_A))
            {
                body.Vx = -Constants.PLAYER_SPEED;
            }
            if (Rl.IsKeyDown(R.KeyboardKey.KEY_D))
            {
                body.Vx = Constants.PLAYER_SPEED;
            }
        }
    }
}
