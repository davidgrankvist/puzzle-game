using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using System.Numerics;

namespace puzzle_game.Game.Entities
{
    public class EntityCreator
    {
        public static IEnumerable<Entity> CreateLevelTiles()
        {
            List<Entity> levelEntities = new List<Entity>();

            var borderTop = new Entity();
            borderTop.AddComponent(new Body(0, -1, new Rectangle(Constants.WINDOW_WIDTH, 1)));
            borderTop.AddComponent(new PhysicsBody());

            var borderLeft = new Entity();
            borderLeft.AddComponent(new Body(-1, 0, new Rectangle(1, Constants.WINDOW_HEIGHT)));
            borderLeft.AddComponent(new PhysicsBody());

            var borderRight = new Entity();
            borderRight.AddComponent(new Body(Constants.WINDOW_WIDTH, 0, new Rectangle(1, Constants.WINDOW_HEIGHT)));
            borderRight.AddComponent(new PhysicsBody());

            levelEntities.Add(borderTop);
            levelEntities.Add(borderLeft);
            levelEntities.Add(borderRight);
            levelEntities.Add(CreateGround());
            levelEntities.Add(CreateObstacle());
            levelEntities.Add(CreateFlyingObstacle());

            return levelEntities;
        }
        public static Entity CreateGround()
        {
            var ground = new Entity();

            const int height = 50;
            const int y = Constants.WINDOW_HEIGHT - height;

            ground.AddComponent(new Body(0, y, new Rectangle(Constants.WINDOW_WIDTH, height)));
            ground.AddComponent(new PhysicsBody());
            ground.AddComponent(new Render(Raylib_cs.Color.BLACK));

            return ground;
        }
        public static Entity CreateObstacle()
        {
            var ground = new Entity();

            const int width = 50;
            const int height = 50;
            const int y = Constants.WINDOW_HEIGHT - 100;

            ground.AddComponent(new Body(400, y, new Rectangle(width, height)));
            ground.AddComponent(new PhysicsBody());
            ground.AddComponent(new Render(Raylib_cs.Color.BLACK));

            return ground;
        }
        public static Entity CreateFlyingObstacle()
        {
            var ground = new Entity();

            const int width = 80;
            const int height = 50;
            const int y = Constants.WINDOW_HEIGHT - 200;

            ground.AddComponent(new Body(450, y, new Rectangle(width, height)));
            ground.AddComponent(new PhysicsBody());
            ground.AddComponent(new Render(Raylib_cs.Color.BLACK));

            return ground;
        }

        public static Entity CreatePlayer()
        {
            var player = new Entity();

            const int width = 10;
            const int height = 15;

            player.AddComponent(new Body(Constants.PLAYER_START_X, Constants.PLAYER_START_Y, new Rectangle(width, height)));
            player.AddComponent(new KeyboardControl());
            player.AddComponent(new PhysicsBody());
            player.AddComponent(new Gravity());
            player.AddComponent(new Render(Raylib_cs.Color.BLUE));

            return player;
        }

        public static Entity CreateCamera()
        {
            var camera = new Entity();
            var cameraComponent = new Camera(
                target: new Vector2(Constants.CAMERA_TARGET_X, Constants.CAMERA_TARGET_Y),
                offset: new Vector2(Constants.CAMERA_TARGET_X, Constants.CAMERA_TARGET_Y),
                rotation: 0,
                zoom: Constants.CAMERA_ZOOM
            );
            camera.AddComponent(cameraComponent);

            return camera;
        }
    }
}
