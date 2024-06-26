﻿using puzzle_game.Game.Common;
using puzzle_game.Game.Components;
using puzzle_game.Game.Mazes;
using System.Numerics;

namespace puzzle_game.Game.Entities
{
    public class EntityCreator
    {
        public static IEnumerable<Entity> CreateMaze(Maze maze)
        {
            var blocks = maze.Blocks;
            return blocks.Select(ToEntity).ToList();
		}

        private static Entity ToEntity(MazeBlock block)
        {
            var entity = new Entity();

            entity.AddComponent(new Body(block.X, block.Y, new Rectangle(block.Width, block.Height)));
            entity.AddComponent(new PhysicsBody());
            entity.AddComponent(new Render(Raylib_cs.Color.BLACK));

            return entity;
        }

        public static Entity CreatePlayer()
        {
            var player = new Entity();

            const float radius = 12;

            player.AddComponent(new Body(GameConstants.PLAYER_START_X, GameConstants.PLAYER_START_Y, new Circle(radius)));
            player.AddComponent(new KeyboardControl());
            player.AddComponent(new PhysicsBody());
            player.AddComponent(new Gravity(GameConstants.PLAYER_GRAVITY));
            player.AddComponent(new Render(Raylib_cs.Color.BLUE));

            return player;
        }

        public static Entity CreateCamera()
        {
            var camera = new Entity();
            var cameraComponent = new Camera(
                target: new Vector2(GameConstants.CAMERA_TARGET_X, GameConstants.CAMERA_TARGET_Y),
                offset: new Vector2(GameConstants.CAMERA_TARGET_X, GameConstants.CAMERA_TARGET_Y),
                rotation: 0,
                zoom: GameConstants.CAMERA_ZOOM
            );
            camera.AddComponent(cameraComponent);

            return camera;
        }
    }
}
