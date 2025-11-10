using Maze.Entities.Player;
using Maze.Services.CameraService;
using Maze.Services.Labyrinth;
using Mvp.Services;
using UnityEngine;
using VContainer.Unity;

namespace Maze.Services.Game
{
    public class CoreGameService : Service, IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly ICameraService _cameraService;
        private readonly ILabyrinthService _labyrinthService;

        public CoreGameService(PlayerFactory playerFactory, ICameraService cameraService, ILabyrinthService labyrinthService)
        {
            _playerFactory = playerFactory;
            _cameraService = cameraService;
            _labyrinthService = labyrinthService;
        }
        
        public void Initialize()
        {
            _playerFactory.CreatePlayer(Vector3.zero);
            _cameraService.SetUpCamera(_labyrinthService);
        }
    }
}