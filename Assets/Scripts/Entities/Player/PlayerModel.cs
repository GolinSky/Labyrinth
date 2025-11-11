using Mvp.Model;

namespace Maze.Entities.Player
{
    public interface IPlayerModelObserver: IModelObserver
    {
        int Steps { get; }
        bool IsMoving { get; }
        float TimeSpent { get; }
    }
    
    public class PlayerModel: Model, IPlayerModelObserver
    {
        public int Steps { get; set; }
        public bool IsMoving { get; set; }
        
        public float TimeSpent { get; set; }
        
        public PlayerModel(PlayerData playerData)
        {
            
        }

       
    }
}