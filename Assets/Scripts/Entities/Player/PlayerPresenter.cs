using System;
using Maze.Services.Game;
using Maze.Services.Input;
using Maze.Services.Labyrinth;
using Maze.Ui;
using Mvp.Presenter;
using UnityEngine;
using Utilities.ScriptUtils.Time;
using VContainer.Unity;

namespace Maze.Entities.Player
{
    public interface IPlayerPresenter: IPresenter
    {
        
    }
    public class PlayerPresenter: Presenter<PlayerModel, PlayerView>, IPlayerPresenter, IInitializable, ITickable, Services.Game.IObserver<CoreGameState>, IDisposable
    {
        private readonly ILabyrinthService _labyrinthService;
        private readonly ICoreGameNotifier _coreGameNotifier;
        private readonly IUiService _uiService;
        private readonly IInputService _inputService;
        private Vector2Int _currentCell;
        private readonly ITimer _moveTimer;
        private CoreGameState _gameState;
        private PlayerUi _playerUi;
        
        
        public PlayerPresenter(
            PlayerModel model,
            PlayerView view,
            ILabyrinthService labyrinthService,
            ICoreGameNotifier coreGameNotifier,
            IUiService uiService,
            IInputService inputService) : base(model, view)
        {
            _labyrinthService = labyrinthService;
            _coreGameNotifier = coreGameNotifier;
            _uiService = uiService;
            _inputService = inputService;
            _moveTimer = TimerFactory.ConstructTimer(Model.MoveDelay);
        }

        public void Initialize()
        {
            _coreGameNotifier.AddObserver(this);
            Vector2Int startCell = new Vector2Int(_labyrinthService.Width / 2, _labyrinthService.Height / 2);
            
            _currentCell = _labyrinthService.FindNearestFloor(startCell);
            SetPosition();


            Model.Steps = 0;
            _playerUi = _uiService.CreateUi<PlayerUi, IPlayerModelObserver>(Model);
            _playerUi.UpdateSteps();
            _playerUi.UpdateTime();
            _playerUi.Show();
        }
        
        public void Dispose()
        {
            _coreGameNotifier.RemoveObserver(this);
        }
        
        public void Tick()
        {
            if(_gameState != CoreGameState.GameIdle) return;
            
            Model.TimeSpent += Time.deltaTime;
            _playerUi.UpdateTime();

            if(!_moveTimer.IsComplete) return;
            
            Vector2 input = _inputService.Move;
            Vector2Int direction = Vector2Int.zero;

            if (input.y > 0.5f) direction = Vector2Int.up;
            else if (input.y < -0.5f) direction = Vector2Int.down;
            else if (input.x < -0.5f) direction = Vector2Int.left;
            else if (input.x > 0.5f) direction = Vector2Int.right;

            Model.IsMoving = direction != Vector2Int.zero;
            if (!Model.IsMoving) return;

            
            Vector2Int target = _currentCell + direction;
            if (!_labyrinthService.IsWalkable(target)) return;

            _currentCell = target;
            SetPosition();
            _moveTimer.StartTimer();
            
            Model.Steps++;
            _playerUi.UpdateSteps();

            if (_labyrinthService.IsExit(_currentCell))
            {
                _coreGameNotifier.NotifyEndGame(Model);
                _playerUi.Hide();
            }
        }

        public void Notify(CoreGameState gameState)
        {
            _gameState = gameState;
        }
        
        private void SetPosition()
        {
            View.SetPosition(_labyrinthService.GetNearestWalkableCell(_currentCell));
        }
    }
}