using Mvp.Model;

namespace Maze.Entities.Labyrinth
{
    public interface ILabyrinthModelObserver : IModelObserver
    {
        int Width { get; }
        int Height { get; }
    }
    public class LabyrinthModel: Model, ILabyrinthModelObserver, ILabyrinthContext
    {
        public int Width { get; }
        public int Height { get; }
        public int ExitCount { get; }
        public float Complexity { get; }
        public float Density { get; }
        
        public LabyrinthModel(ILabyrinthContext labyrinthContext)
        {
            Width = labyrinthContext.Width;
            Height = labyrinthContext.Height;
            ExitCount = labyrinthContext.ExitCount;
            Complexity = labyrinthContext.Complexity;
            Density = labyrinthContext.Density;
        }
    }
}