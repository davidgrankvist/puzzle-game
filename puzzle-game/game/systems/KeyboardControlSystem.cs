using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;
using Raylib_cs;
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
            HandleReset();
			UpdateCamera();
            UpdatePlayer();
        }

		private void HandleReset()
		{

			var player = Entities.Find((entity) => entity.HasComponent<KeyboardControl>());
			if (player == null)
			{
				return;
			}
			var body = player.GetComponentUnsafe<Body>();
			var physicsBody = player.GetComponentUnsafe<PhysicsBody>();

			if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
			{
                body.X = Constants.PLAYER_START_X;
                body.Y = Constants.PLAYER_START_Y;
                physicsBody.Vx = 0;
                physicsBody.Vy = 0;
			}
		}

		void UpdateCamera()
        {
            var cameraEntity = Entities.Find(entity => entity.HasComponent<Camera>());
            if (cameraEntity == null)
            {
                return;
            }
            var camera = cameraEntity.GetComponentUnsafe<Camera>();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                camera.Rotation -= Constants.CAMERA_ROTATION_SPEED;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
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

            var cameraEntity = Entities.Find(entity => entity.HasComponent<Camera>());
            if (cameraEntity == null)
            {
                return;
            }

			var vx = body.Vx;
			var vy = body.Vy;

			var camera = cameraEntity.GetComponentUnsafe<Camera>();
            var angle = -camera.Rotation * MathF.PI / 180f;

			var angleHorizontalMovement = angle + MathF.PI / 2;
			var angleJumpMovement = angle + MathF.PI;

			var vxNew = vx;
			var vyNew = vy;

			// ------ left/right -------

			var horizontalDeltaX = -MathF.Sin(angleHorizontalMovement) * Constants.PLAYER_SPEED;
			var horizontalDeltaY = MathF.Cos(angleHorizontalMovement) * Constants.PLAYER_SPEED;

			// start moving left/right
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
			{
				vxNew += horizontalDeltaX;
				vyNew += horizontalDeltaY;
			}
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
			{
				vxNew -= horizontalDeltaX;
				vyNew -= horizontalDeltaY;
			}

			// stop moving left/right
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_A))
			{
				if (vxNew != 0)
				{
					vxNew -= horizontalDeltaX;
				}
				if (vyNew != 0)
				{
					vyNew -= horizontalDeltaY;
				}
			}
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_D))
			{
				if (vxNew != 0)
				{
					vxNew += horizontalDeltaX;
				}
				if (vyNew != 0)
				{
					vyNew += horizontalDeltaY;
				}
			}

			// ------ jump -------

			var jumpDeltaX = -MathF.Sin(angleJumpMovement) * Constants.PLAYER_JUMP_SPEED;
			var jumpDeltaY = MathF.Cos(angleJumpMovement) * Constants.PLAYER_JUMP_SPEED;

			// jump if stationary along y axis
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) && vy < 0.1)
			{
				vxNew += jumpDeltaX;
				vyNew += jumpDeltaY;
			}

			body.Vx = vxNew;
			body.Vy = vyNew;
		}
	}
}
