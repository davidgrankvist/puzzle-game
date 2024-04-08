using puzzle_game.Game.Entities;
using puzzle_game.Game.Mazes;

namespace puzzle_game.Game.Systems
{
    public class SpawnSystem : ISystem
    {
        List<Entity> Entities;
        IMazeProvider mazeProvider;

        public SpawnSystem(List<Entity> entities, IMazeProvider mazeProvider)
        {
            Entities = entities;
            this.mazeProvider = mazeProvider;
        }

        public void Load()
        {
            Entities.AddRange(EntityCreator.CreateMaze(mazeProvider.GetMaze()));
            Entities.Add(EntityCreator.CreatePlayer());
            Entities.Add(EntityCreator.CreateCamera());
        }

        public void Update()
        {
        }
    }
}
