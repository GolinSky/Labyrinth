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
        void RestartGame();
        void ExitGame();
    }

    public class CoreGameService : Service, IInitializable, ICoreGameNotifier, ICoreGamePresenter
    {
        private readonly PlayerFactory _playerFactory;
        private readonly ICameraService _cameraService;
        private readonly ILabyrinthService _labyrinthService;
        private readonly IUiService _uiService;
        private readonly ScoreService _scoreService;
        private readonly IGameService _gameService;

        private readonly List<IObserver<CoreGameState>> _observers = new();
        private MazeSetUpUi _mazeSetUpUi;

        public CoreGameService(
            IGameService gameService,
            PlayerFactory playerFactory,
            ICameraService cameraService,
            ILabyrinthService labyrinthService,
            IUiService uiService,
            ScoreService scoreService)
        {
            _gameService = gameService;
            _playerFactory = playerFactory;
            _cameraService = cameraService;
            _labyrinthService = labyrinthService;
            _uiService = uiService;
            _scoreService = scoreService;
        }
        
        public void Initialize()
        {
            _mazeSetUpUi = _uiService.CreatePresenterBasedUi<MazeSetUpUi, ICoreGamePresenter>(this);
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
            EndGameUi endGameUi = _uiService.CreatePresenterBasedUi<EndGameUi, ICoreGamePresenter>(this);
            endGameUi.SetData(playerModel.Steps, playerModel.TimeSpent);
            endGameUi.Show();
            _scoreService.SaveScore(matchDuration: playerModel.TimeSpent, steps: playerModel.Steps);
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

        public void RestartGame()
        {
            _gameService.RestartCoreGame();
        }

        public void ExitGame()
        {
            _gameService.LoadMenu();
        }
        
        private void Notify(CoreGameState state)
        {
            for (var i = 0; i < _observers.Count; i++)
            {
                _observers[i].Notify(state);
            }
        }
    }
}