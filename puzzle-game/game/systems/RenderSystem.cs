using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Entities;

namespace puzzle_game.Game.Systems
{
    public class RenderSystem : ISystem
    {
        private List<Entity> entities;

        public RenderSystem(List<Entity> entities)
        {
            this.entities = entities;
        }

        public void Load()
        {
        }

        public void Update()
        {
            foreach (var entity in entities)
            {
                RenderEntity(entity);
            }
        }

        private void RenderEntity(Entity entity)
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
                Raylib_cs.Raylib.DrawRectangle((int)body.X, (int)body.Y, rect.Width, rect.Height, render.FillColor);
            } else if (body.Shape is Circle)
            {
                var circle = (Circle)body.Shape;
				Raylib_cs.Raylib.DrawCircle((int)body.X, (int)body.Y, circle.Radius, render.FillColor);
			}
		}
    }
}
