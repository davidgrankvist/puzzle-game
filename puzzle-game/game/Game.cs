using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;
using puzzle_game.Game.Mazes;
using puzzle_game.Game.Systems;
using Raylib_cs;

namespace puzzle_game.Game
{
    public class Game
	{
		private List<Entity> entities;
		private List<ISystem> systems;
		private Camera? camera;


		public Game()
		{
			entities = new List<Entity>();
			var mazeProvider = new TestMazeProvider();

			systems = new List<ISystem>
			{
				new SpawnSystem(entities, mazeProvider),
				new KeyboardControlSystem(entities),
				new PhysicsSystem(entities),
				new RenderSystem(entities)
			};
		}

		public void Run()
		{
			foreach (var system in systems)
			{
				system.Load();
			}
			InitializeCamera();
			GameLoop();
		}

		private void InitializeCamera()
		{
			var cameraEntity = entities.Find(entity => entity.HasComponent<Camera>());
			if (cameraEntity == null)
			{
				throw new InvalidOperationException("No camera entity found. Please make sure that the spawn system is loaded.");
			}
			camera = cameraEntity.GetComponentUnsafe<Camera>();
		}

		private void GameLoop()
		{
			Raylib.InitWindow(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT, Constants.WINDOW_TITLE);
			Raylib.SetTargetFPS(60);

			while (!Raylib.WindowShouldClose())
			{
				Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.RAYWHITE);
				Raylib.BeginMode2D(camera!.RCamera);

				foreach (var system in systems)
				{
					system.Update();
				}

				Raylib.EndMode2D();
				Raylib.EndDrawing();
			}

			Raylib.CloseWindow();
		}
	}
}
