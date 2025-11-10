using Fps.MVP.Services;
using Maze.Entities.Player;
using UnityEngine;
using VContainer.Unity;

namespace Fps.Services.Game
{
    public class CoreGameService:Service, IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        
        public CoreGameService(PlayerFactory playerFactory)
        {
           _playerFactory = playerFactory;
        }
        
        public void Initialize()
        {
            _playerFactory.CreatePlayer(Vector3.zero);
        }
    }
}