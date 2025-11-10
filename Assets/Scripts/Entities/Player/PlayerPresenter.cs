using Maze.Entities.Labyrinth;
using Mvp.Presenter;
using UnityEngine;
using VContainer.Unity;

namespace Maze.Entities.Player
{
    public interface IPlayerPresenter: IPresenter
    {
        
    }
    public class PlayerPresenter: Presenter<PlayerModel, PlayerView>, IPlayerPresenter, IInitializable
    {
        public PlayerPresenter(PlayerModel model, PlayerView view) : base(model, view)
        {
          
        }

        public void Initialize()
        {
            
        }
    }
}