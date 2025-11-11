using UnityEngine;

namespace Maze.Services.Labyrinth
{
    public interface ILabyrinthProvider
    {
        int Width { get; }
        int Height { get; }
        bool IsWalkable(Vector2Int cell);
        bool IsExit(Vector2Int cell);
        Vector3 GetNearestWalkableCell(Vector2Int from);
        Vector2Int FindNearestFloor(Vector2Int center);
        Vector3 GetWorldCoordinates(Vector2Int from);
    }
}