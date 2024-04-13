namespace puzzle_game.Game.Common
{
    public static class GameConstants
    {
        public const int WINDOW_WIDTH = 1920;
        public const int WINDOW_HEIGHT = 974;
        public const string WINDOW_TITLE = "Puzzle game";

		public const float CENTER_X = WINDOW_WIDTH / 2;
		public const float CENTER_Y = WINDOW_HEIGHT / 2;

		public const float PLAYER_SPEED = 5;
        public const float PLAYER_JUMP_SPEED = PLAYER_SPEED * 3;
        public const float PLAYER_GRAVITY = 1;
		public const float PLAYER_START_X = CENTER_X - 20;
        public const float PLAYER_START_Y = CENTER_Y - 20;

		public const float CAMERA_ROTATION_SPEED = 0.5f;
        public const float CAMERA_TARGET_X = CENTER_X;
        public const float CAMERA_TARGET_Y = CENTER_Y;
        public const float CAMERA_ZOOM = 1f;

        public const int MAZE_ROWS = 20;
		public const int MAZE_COLS = 20;
	}
}
