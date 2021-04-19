namespace MarsRoverCore
{
    public interface IMap
    {
        bool HasObstacleAt(Coordinates coordinates);

        Coordinates NextCoordinateTo(Coordinates coordinates, Direction direction);
    }
}