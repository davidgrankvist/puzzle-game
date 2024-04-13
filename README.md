# puzzle-game
A game about rotating mazes.

## About

This is a mix of a ball-in-a-maze puzzle and a platformer:
- the player can control the ball to move left, right or jump
- the player can rotate the entire maze
- gravity keeps pointing downward as the maze rotates

## Implementation

The game is built with [Raylib](https://www.raylib.com/) and uses an [ECS](https://en.wikipedia.org/wiki/Entity_component_system) structure. The mazes are generated using randomized depth-first search (see [this Wikipedia article](https://en.wikipedia.org//wiki/Maze_generation_algorithm) for a great summary of different approaches).