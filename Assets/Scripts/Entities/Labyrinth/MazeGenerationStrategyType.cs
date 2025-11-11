using System;

namespace Maze.Entities.Labyrinth
{
    [Serializable]
    public enum MazeGenerationStrategyType
    {
        Kruskal = 0,// not sure it is dfs
        Prim = 1,
        
    }
}