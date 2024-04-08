using puzzle_game.Game.Entities;
using puzzle_game.Game.Mazes;

namespace puzzle_game.Game.Systems
{
    public class SpawnSystem : ISystem
    {
        private List<Entity> entities;
        private IMazeProvider mazeProvider;

        public SpawnSystem(List<Entity> entities, IMazeProvider mazeProvider)
        {
            this.entities = entities;
            this.mazeProvider = mazeProvider;
        }

        public void Load()
        {
            entities.AddRange(EntityCreator.CreateMaze(mazeProvider.GetMaze()));
            entities.Add(EntityCreator.CreatePlayer());
            entities.Add(EntityCreator.CreateCamera());
        }

        public void Update()
        {
        }
    }
}
