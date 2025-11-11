using System.Collections.Generic;
using Maze.Entities.Labyrinth;
using Maze.Entities.Player;
using Maze.Services.CameraService;
using Maze.Services.Labyrinth;
using Maze.Ui;
using Maze.Ui.EndGame;
using Maze.Ui.MazeSetUp;
using Mvp.Presenter;
using Mvp.Services;
using UnityEngine;
using VContainer.Unity;

namespace Maze.Services.Game
{
    public interface ICoreGamePresenter: IPresenter
    {
        void StartGame(ILabyrinthContext labyrinthContext);
    }

    public class CoreGameService : Service, IInitializable, ICoreGameNotifier, ICoreGamePresenter
    {
        private readonly PlayerFactory _playerFactory;
        private readonly ICameraService _cameraService;
        private readonly ILabyrinthService _labyrinthService;
        private readonly IUiService _uiService;

        private readonly List<IObserver<CoreGameState>> _observers = new();
        private MazeSetUpUi _mazeSetUpUi;

        public CoreGameService(
            PlayerFactory playerFactory,
            ICameraService cameraService,
            ILabyrinthService labyrinthService,
            IUiService uiService)
        {
            _playerFactory = playerFactory;
            _cameraService = cameraService;
            _labyrinthService = labyrinthService;
            _uiService = uiService;
        }
        
        public void Initialize()
        {
            _mazeSetUpUi = _uiService.CreateUi<MazeSetUpUi>();
            _mazeSetUpUi.AssignPresenter(this); 
            _mazeSetUpUi.Show();
        }

        public void AddObserver(IObserver<CoreGameState> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver<CoreGameState> observer)
        {
            _observers.Remove(observer);
        }
        
        public void NotifyEndGame(IPlayerModelObserver playerModel)
        {
            Notify(CoreGameState.GameEnd);
            EndGameUi endGameUi = _uiService.CreateUi<EndGameUi>();
            endGameUi.SetData(playerModel.Steps, playerModel.TimeSpent);
            endGameUi.Show();
        }

        private void Notify(CoreGameState state)
        {
            for (var i = 0; i < _observers.Count; i++)
            {
                _observers[i].Notify(state);
            }
        }
        
        public void StartGame(ILabyrinthContext labyrinthContext)
        {
            _mazeSetUpUi.Hide();
            _labyrinthService.ConstructMaze(labyrinthContext);
            _playerFactory.CreatePlayer(Vector3.zero);
            _cameraService.SetUpCamera(_labyrinthService);
            Notify(CoreGameState.GameStarted);
            Notify(CoreGameState.GameIdle);
        }
    }
}