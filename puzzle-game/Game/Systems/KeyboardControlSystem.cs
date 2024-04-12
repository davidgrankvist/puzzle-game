using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;
using Raylib_cs;
using System.Numerics;

namespace puzzle_game.Game.Systems
{
    public class KeyboardControlSystem : ISystem
    {
        private List<Entity> entities;

        public KeyboardControlSystem(List<Entity> entities)
        {
            this.entities = entities;
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

			var player = entities.Find((entity) => entity.HasComponent<KeyboardControl>());
			if (player == null)
			{
				return;
			}
			var body = player.GetComponentUnsafe<Body>();
			var physicsBody = player.GetComponentUnsafe<PhysicsBody>();

			if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
			{
                body.X = GameConstants.PLAYER_START_X;
                body.Y = GameConstants.PLAYER_START_Y;
                physicsBody.Vx = 0;
                physicsBody.Vy = 0;
			}
		}

		void UpdateCamera()
        {
            var cameraEntity = entities.Find(entity => entity.HasComponent<Camera>());
            if (cameraEntity == null)
            {
                return;
            }
            var camera = cameraEntity.GetComponentUnsafe<Camera>();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                camera.Rotation -= GameConstants.CAMERA_ROTATION_SPEED;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                camera.Rotation += GameConstants.CAMERA_ROTATION_SPEED;
			}
		}

        void UpdatePlayer()
        {
            var player = entities.Find((entity) => entity.HasComponent<KeyboardControl>());
            if (player == null)
            {
                return;
            }

            var body = player.GetComponent<PhysicsBody>();

            if (body == null)
            {
                return;
            }

            var cameraEntity = entities.Find(entity => entity.HasComponent<Camera>());
            if (cameraEntity == null)
            {
                return;
            }

			var vx = body.Vx;
			var vy = body.Vy;

			var camera = cameraEntity.GetComponentUnsafe<Camera>();
            var angle = -camera.RotationRadians;

			var angleHorizontalMovement = angle + MathF.PI / 2;
			var angleJumpMovement = angle + MathF.PI;

			var vxNew = vx;
			var vyNew = vy;

			// ------ left/right -------

			var horizontalDeltaX = -MathF.Sin(angleHorizontalMovement) * GameConstants.PLAYER_SPEED;
			var horizontalDeltaY = MathF.Cos(angleHorizontalMovement) * GameConstants.PLAYER_SPEED;

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

			var jumpDeltaX = -MathF.Sin(angleJumpMovement) * GameConstants.PLAYER_JUMP_SPEED;
			var jumpDeltaY = MathF.Cos(angleJumpMovement) * GameConstants.PLAYER_JUMP_SPEED;

			var isVertical = Math.Abs(MathF.Sin(angle)) > 0.5;
			var isOnSurface = isVertical ? vxNew == 0 : vyNew == 0;

			if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) && isOnSurface)
			{
				vxNew += jumpDeltaX;
				vyNew += jumpDeltaY;
			}

			body.Vx = vxNew;
			body.Vy = vyNew;
		}
	}
}
