using System;
using Mvp.Services;
using UnityEngine;

namespace Maze.Services.Input
{
    public interface IInputService: IService
    {
        Vector2 Move { get; }
    }

    public class InputService : Service, IDisposable, IInputService
    {
        private readonly CoreInputActions _actions;

        public Vector2 Move => _actions.Player.Move.ReadValue<Vector2>();

        
        public InputService()
        {
            _actions = new CoreInputActions();
            _actions.Enable();
        }


        public void Dispose()
        {
            _actions.Disable();
        }
    }
    
}