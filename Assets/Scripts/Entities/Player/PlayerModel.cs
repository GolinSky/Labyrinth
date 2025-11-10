using Fps.MVP.Model;

namespace Maze.Entities.Player
{
    public interface IPlayerModelObserver: IModelObserver
    {
        
    }
    
    public class PlayerModel: Model, IPlayerModelObserver
    {
        public PlayerModel(PlayerData playerData)
        {
            
        }
    }
}