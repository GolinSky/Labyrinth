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

    public interface IGenerationContext
    {
        MazeGenerationStrategyType GenerationStrategyType { get; }
    }
    
    [CreateAssetMenu(fileName = "LabyrinthData", menuName = "Data/LabyrinthData")]
    public class LabyrinthData: Data, ILabyrinthContext, IGenerationContext
    {
        [Header("Maze Settings")]
        [field:Range(3, 50)] 
        [field: SerializeField] public int Width { get; private set; }

        [field:Range(3, 50)] 
        [field: SerializeField] public int Height { get; private set; }
        
        [field:Range(1, 5)] 
        [field: SerializeField] public int ExitCount { get; private set; }

        [field:Range(0f, 1f)]
        [field: SerializeField] public float Complexity { get; private set; }

        [field:Range(0f, 1f)]
        [field: SerializeField] public float Density { get; private set; }
        [field: SerializeField] public MazeGenerationStrategyType GenerationStrategyType { get; private set; }
    }
}