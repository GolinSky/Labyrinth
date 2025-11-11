using Mvp.Model;
using UnityEngine;

namespace Maze.Entities.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : Data
    {
        [Range(0.1f, 1f)]
        [field: SerializeField]
        public float MoveDelay { get; private set; } = 0.1f;
    }
}