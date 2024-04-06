using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;
using puzzle_game.Game.Systems;
using System.Numerics;

namespace puzzle_game.Game
{
    public class Game
	{
		List<Entity> Entities;
		List<ISystem> Systems;
		Camera Camera;


		public Game()
		{
			Entities = new List<Entity>();

			Systems = new List<ISystem>
			{
				new SpawnSystem(Entities),
				new KeyboardControlSystem(Entities),
				new PhysicsSystem(Entities),
				new RenderSystem(Entities)
			};
		}

		public void Run()
		{
			foreach (var system in Systems)
			{
				system.Load();
			}
			InitializeCamera();
			GameLoop();
		}

		void InitializeCamera()
		{
			var cameraEntity = Entities.Find(entity => entity.HasComponent<Camera>());
			if (cameraEntity == null)
			{
				throw new InvalidOperationException("No camera entity found. Please make sure that the spawn system is loaded.");
			}
			Camera = cameraEntity.GetComponentUnsafe<Camera>();
		}

		void GameLoop()
		{
			Rl.InitWindow(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT, Constants.WINDOW_TITLE);
			Rl.SetTargetFPS(60);

			while (!Rl.WindowShouldClose())
			{
				Rl.BeginDrawing();
				Rl.ClearBackground(R.Color.RAYWHITE);
				Rl.BeginMode2D(Camera.RCamera);

				foreach (var system in Systems)
				{
					system.Update();
				}

				Rl.EndMode2D();
				Rl.EndDrawing();
			}

			Rl.CloseWindow();
		}
	}
}
