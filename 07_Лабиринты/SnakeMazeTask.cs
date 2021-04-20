namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int i = 0;
            while (!robot.Finished && i < 1000)
            {
                MoveTo(robot, Direction.Right, width - 3);
                MoveTo(robot, Direction.Down, 2);
                MoveTo(robot, Direction.Left, width - 3);
                MoveTo(robot, Direction.Down, 2);
                i++;
            }
        }
		
        public static void MoveTo(Robot robot, Direction direction, int path)
        {
            for (int i = 0; i < path && !robot.Finished; i++)
                robot.MoveTo(direction);
        }
    }
}
