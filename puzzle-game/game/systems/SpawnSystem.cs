
namespace puzzle_game
{
	public class SpawnSystem : ISystem
	{
		List<Entity> Entities;

		public SpawnSystem(List<Entity> entities)
		{
			Entities = entities;
		}

		public void Load()
		{
			Entities.AddRange(EntityCreator.CreateLevelTiles());
			Entities.Add(EntityCreator.CreatePlayer());
			Entities.Add(EntityCreator.CreateCamera());
		}

		public void Update()
		{
		}
	}
}
