namespace Maze.Services.Game
{
    public interface IObserver<in T>
    {
        void Notify(T arg);
    }
}