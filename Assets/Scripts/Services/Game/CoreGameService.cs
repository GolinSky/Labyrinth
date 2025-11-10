using Maze.Entities.Player;
using Mvp.Services;
using UnityEngine;
using VContainer.Unity;

namespace Maze.Services.Game
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