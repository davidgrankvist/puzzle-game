using puzzle_game.Game.Components;

namespace puzzle_game.Game.Entities
{
    public class Entity
    {
        public Guid Id { get; }
        List<IComponent> Components;

        public Entity()
        {
            Id = Guid.NewGuid();
            Components = new List<IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return Components.OfType<T>().Any();
        }

        public T? GetComponent<T>() where T : IComponent
        {
            return Components.OfType<T>().FirstOrDefault();

        }
        public T GetComponentUnsafe<T>() where T : IComponent
        {
            return Components.OfType<T>().First();
        }
    }
}
