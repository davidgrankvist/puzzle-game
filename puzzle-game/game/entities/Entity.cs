using puzzle_game.Game.Components;

namespace puzzle_game.Game.Entities
{
    public class Entity
    {
        public Guid Id { get; }
        private List<IComponent> components;

        public Entity()
        {
            Id = Guid.NewGuid();
            components = new List<IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return components.OfType<T>().Any();
        }

        public T? GetComponent<T>() where T : IComponent
        {
            return components.OfType<T>().FirstOrDefault();

        }
        public T GetComponentUnsafe<T>() where T : IComponent
        {
            return components.OfType<T>().First();
        }
    }
}
