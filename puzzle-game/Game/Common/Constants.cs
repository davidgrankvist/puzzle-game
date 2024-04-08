﻿namespace puzzle_game.Game.Common
{
    public static class Constants
    {
        public const int WINDOW_WIDTH = 1000;
        public const int WINDOW_HEIGHT = 500;
        public const string WINDOW_TITLE = "tee hee puzzles";

        public const float PLAYER_SPEED = 5;
        public const float PLAYER_JUMP_SPEED = PLAYER_SPEED * 3;
        public const float PLAYER_GRAVITY = 1;
		public const float PLAYER_START_X = 80f;
        public const float PLAYER_START_Y = WINDOW_HEIGHT / 2;

		public const float CAMERA_ROTATION_SPEED = 0.5f;
        public const float CAMERA_TARGET_X = WINDOW_WIDTH / 2;
        public const float CAMERA_TARGET_Y = WINDOW_HEIGHT / 2;
        public const float CAMERA_ZOOM = 0.5f;
	}
}