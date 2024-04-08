using puzzle_game.Game.Common;
using Raylib_cs;
using System.Numerics;

namespace puzzle_game.Game.Components
{
    public class Camera : IComponent
    {
        private Camera2D camera;

        public Camera2D RCamera { get => camera; }

        public float Rotation { get => camera.Rotation; set => camera.Rotation = value; }

		public float RotationRadians { get => Rotation * MathHelpers.DEGREES_TO_RADIANS; }

        public Camera(Vector2 target, Vector2 offset, float rotation, float zoom)
        {
            camera = new Camera2D();
            camera.Target = target;
            camera.Offset = offset;
            camera.Rotation = rotation;
            camera.Zoom = zoom;
        }
    }
}
