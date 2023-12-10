
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
			Entities.Add(EntityCreator.CreateGround());
			Entities.Add(EntityCreator.CreatePlayer());
		}

		public void Update()
		{
		}
	}
}
