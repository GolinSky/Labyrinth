using System.Collections.Generic;
using UnityEngine;

namespace Maze.Entities.Labyrinth
{
    public struct GeneratedLabyrinthData
    {
        public int[,] Maze { get; private set; }
        public List<Vector2Int> Exits { get; private set;}

        public GeneratedLabyrinthData(int[,] maze, List<Vector2Int> exits)
        {
            Maze = maze;
            Exits = exits;
        }
    }
}