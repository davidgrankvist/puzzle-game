using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;

namespace puzzle_game.Game.Systems
{
    class RenderSystem : ISystem
    {
        List<Entity> Entities;

        public RenderSystem(List<Entity> entities)
        {
            Entities = entities;
        }

        public void Load()
        {
        }

        public void Update()
        {
            foreach (var entity in Entities)
            {
                renderEntity(entity);
            }
        }

        void renderEntity(Entity entity)
        {
            var render = entity.GetComponent<Render>();
            var body = entity.GetComponent<Body>();

            if (render == null || body == null)
            {
                return;
            }

            if (body.Shape is Rectangle)
            {
                var rect = (Rectangle)body.Shape;
                Raylib_cs.Raylib.DrawRectangle(body.X, body.Y, rect.Width, rect.Height, render.FillColor);
            }
        }
    }
}
