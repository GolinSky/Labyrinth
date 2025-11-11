namespace Maze.Entities.Labyrinth
{
    public interface ILabyrinthGenerator
    {
        GeneratedLabyrinthData GenerateMaze(int width, int height, float complexity, float density, int exitCount);
    }
}