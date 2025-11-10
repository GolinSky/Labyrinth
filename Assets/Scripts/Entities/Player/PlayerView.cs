using Maze.View;
using UnityEngine;

namespace Maze.Entities.Player
{
    public class PlayerView: View<IPlayerModelObserver, IPlayerPresenter>
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}