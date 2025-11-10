using System;
using Maze.Services.Game;
using Maze.Services.Labyrinth;
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
        private Vector2Int _currentCell;
        private readonly ITimer _moveTimer;
        private CoreGameState _gameState;

        public PlayerPresenter(
            PlayerModel model,
            PlayerView view,
            ILabyrinthService labyrinthService,
            ICoreGameNotifier coreGameNotifier) : base(model, view)
        {
            _labyrinthService = labyrinthService;
            _coreGameNotifier = coreGameNotifier;
            _moveTimer = TimerFactory.ConstructTimer(0.1f);
        }

        public void Initialize()
        {
            _coreGameNotifier.AddObserver(this);
            // Start at maze center
            Vector2Int startCell = new Vector2Int(_labyrinthService.Width / 2, _labyrinthService.Height / 2);
            
            _currentCell = _labyrinthService.FindNearestFloor(startCell);
            SetPosition();
        }
        
        public void Dispose()
        {
            _coreGameNotifier.RemoveObserver(this);

        }
        
        public void Tick()
        {
            if(_gameState != CoreGameState.GameIdle) return;
            if(!_moveTimer.IsComplete) return;
            
            Vector2Int direction = Vector2Int.zero;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) direction = Vector2Int.up;
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) direction = Vector2Int.down;
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) direction = Vector2Int.left;
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) direction = Vector2Int.right;

            if (direction != Vector2Int.zero)
            {
                Vector2Int target = _currentCell + direction;
                if (_labyrinthService.IsWalkable(target))
                {
                    _currentCell = target;
                    SetPosition();
                 //   StartCoroutine(MoveCooldown());
                    _moveTimer.StartTimer();
                 
                    if (_labyrinthService.IsExit(_currentCell))
                    {
                        _coreGameNotifier.Notify(CoreGameState.GameEnd);
                        Debug.Log("🎉 You found the exit!");
                    }
                }
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