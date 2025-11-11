using Maze.Entities.Player;

namespace Maze.Services.Game
{
    public interface ICoreGameNotifier
    {
        void AddObserver(IObserver<CoreGameState> observer);
        void RemoveObserver(IObserver<CoreGameState> observer);

        void NotifyEndGame(IPlayerModelObserver playerModel);
    }
}