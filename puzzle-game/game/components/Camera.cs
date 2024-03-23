using Raylib_cs;
using System.Numerics;

namespace puzzle_game
{
	class Camera : IComponent
	{
		private Camera2D camera;

		public Camera2D RCamera { get => camera; }

		public float Rotation { get => camera.Rotation; set => camera.Rotation = value; }

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
