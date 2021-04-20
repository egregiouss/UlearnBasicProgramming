namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
                MoveTo(robot, Direction.Right, width - 3);
                MoveTo(robot, Direction.Down,  height - 3);
        }

        public static void MoveTo(Robot robot, Direction direction, int path)
        {
            for (int i = 0; i < path && !robot.Finished; i++)
                robot.MoveTo(direction);
        }
    }
}
