using Mvp.Model;
using UnityEngine;

namespace Maze.Entities.Labyrinth
{
    public interface ILabyrinthContext
    {
        int Width { get; }
        int Height { get; }
        int ExitCount { get; }
        float Complexity { get; }
        float Density { get; }
    }
    
    [CreateAssetMenu(fileName = "LabyrinthData", menuName = "Data/LabyrinthData")]
    public class LabyrinthData: Data, ILabyrinthContext
    {
        [Header("Maze Settings")]
        [field: SerializeField] public int Width { get; private set; }

        [field: SerializeField] public int Height { get; private set; }
        [field:Range(1, 5)] 
        [field: SerializeField] public int ExitCount { get; private set; }

        [field:Range(0f, 1f)]
        [field: SerializeField] public float Complexity { get; private set; }

        [field:Range(0f, 1f)]
        [field: SerializeField] public float Density { get; private set; }
    }

  
}