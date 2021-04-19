namespace MarsRoverCore
{
    public class ExecutionResult
    {
        public string Position { get; set; }
        public bool ObstacleFound { get; set; }

        public ExecutionResult(string position)
        {
            Position = position;
        }

        public ExecutionResult(string position, bool hasFoundObstacle)
        {
            Position = position;
            ObstacleFound = hasFoundObstacle;
        }
    }
}