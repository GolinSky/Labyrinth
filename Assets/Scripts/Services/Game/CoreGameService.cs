using System.Collections.Generic;
using Maze.Entities.Player;
using Maze.Services.CameraService;
using Maze.Services.Labyrinth;
using Mvp.Services;
using UnityEngine;
using VContainer.Unity;

namespace Maze.Services.Game
{
    public interface ICoreGameNotifier
    {
        void AddObserver(IObserver<CoreGameState> observer);
        void RemoveObserver(IObserver<CoreGameState> observer);
        void Notify(CoreGameState state);
    }
    
    public class CoreGameService : Service, IInitializable, ICoreGameNotifier
    {
        private readonly PlayerFactory _playerFactory;
        private readonly ICameraService _cameraService;
        private readonly ILabyrinthService _labyrinthService;
    
        private readonly List<IObserver<CoreGameState>> _observers = new();
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
            Notify(CoreGameState.GameStarted);
            Notify(CoreGameState.GameIdle);
        }

        public void AddObserver(IObserver<CoreGameState> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver<CoreGameState> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(CoreGameState state)
        {
            for (var i = 0; i < _observers.Count; i++)
            {
                _observers[i].Notify(state);
            }
        }
    }
}