namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (!robot.Finished)
                DiagonalFormMaze(robot, width - 2, height - 2);
        }

        public static int GetRatioOfParties(int xMax, int yMax)
        {
            return xMax > yMax ? (xMax / yMax) : (yMax / xMax);
        }

        public static void DiagonalFormMaze(Robot robot, int xMax, int yMax)
        {
            if (xMax > yMax) 
                TryToMove(robot, GetRatioOfParties(xMax, yMax), Direction.Right, Direction.Down);
            else 
                TryToMove(robot, GetRatioOfParties(xMax, yMax), Direction.Down, Direction.Right);
        }

        public static void TryToMove(Robot robot, int diagonalForm, Direction directionOne, Direction directionTwo)
        {	
            for (int i = 0; i < diagonalForm && !robot.Finished; i++)
                robot.MoveTo(directionOne);
            if (!robot.Finished) robot.MoveTo(directionTwo);
        }
    }
}
