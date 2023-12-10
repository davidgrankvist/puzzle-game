namespace puzzle_game
{
    public class Game
    {
        List<Entity> Entities;
        List<ISystem> Systems;

        public Game() {
            Entities = new List<Entity>();
            Systems = new List<ISystem>
            {
                new SpawnSystem(Entities),
                new KeyboardControlSystem(Entities),
                new RenderSystem(Entities)
            };
		}

        public void Run()
        {
            foreach(var system in Systems)
            {
                system.Load();
            }
            GameLoop();
        }

        void GameLoop()
        {
            Rl.InitWindow(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT, Constants.WINDOW_TITLE);
            Rl.SetTargetFPS(60);

            while(!Rl.WindowShouldClose())
            {
				Rl.BeginDrawing();
                Rl.ClearBackground(R.Color.RAYWHITE);

                foreach(var system in Systems)
                {
                    system.Update();
                }

                Rl.EndDrawing();
            }

            Rl.CloseWindow();
        }
    }
}
