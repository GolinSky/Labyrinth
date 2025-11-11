using System.Collections.Generic;
using UnityEngine;

namespace Maze.Entities.Labyrinth
{
    public class PrimLabyrinthGenerator : ILabyrinthGenerator
    {
        private GeneratedLabyrinthData _generatedLabyrinthData;
        private readonly List<Vector2Int> _exits = new();
        private int[,] _maze;

        public GeneratedLabyrinthData GenerateMaze(int width, int height, float complexity, float density, int exitCount)
        {
            _maze = GenerateMazeInternal(width, height, complexity, density);
            PlaceExits(exitCount, height, width);
            _generatedLabyrinthData = new(_maze, _exits);
            return _generatedLabyrinthData;
        }

        private int[,] GenerateMazeInternal(int width, int height, float complexity, float density)
        {
            int[,] maze = new int[width, height];
            System.Random rnd = new();

            // start in the middle
            Vector2Int start = new(width / 2, height / 2);
            maze[start.x, start.y] = 1;

            // store frontier cells
            List<Vector2Int> frontiers = new();

            void AddFrontiers(Vector2Int pos)
            {
                foreach (var dir in Directions)
                {
                    Vector2Int next = pos + dir * 2;
                    if (InBounds(next, width, height) && maze[next.x, next.y] == 0)
                    {
                        maze[next.x, next.y] = -1; // mark as frontier
                        frontiers.Add(next);
                    }
                }
            }

            AddFrontiers(start);

            int targetCells = (int)(width * height * density);
            int carvedCells = 1;

            while (frontiers.Count > 0 && carvedCells < targetCells)
            {
                int randIndex = rnd.Next(frontiers.Count);
                Vector2Int current = frontiers[randIndex];
                frontiers.RemoveAt(randIndex);

                // find any carved neighbor 2 cells away
                List<Vector2Int> neighbors = new();
                foreach (var dir in Directions)
                {
                    Vector2Int neighbor = current + dir * 2 * -1;
                    if (InBounds(neighbor, width, height) && maze[neighbor.x, neighbor.y] == 1)
                        neighbors.Add(neighbor);
                }

                if (neighbors.Count > 0)
                {
                    Vector2Int connect = neighbors[rnd.Next(neighbors.Count)];
                    Vector2Int mid = (current + connect) / 2;
                    maze[current.x, current.y] = 1;
                    maze[mid.x, mid.y] = 1;
                    carvedCells += 2;

                    AddFrontiers(current);

                    // occasionally add extra branches for lower complexity
                    if (rnd.NextDouble() < (1f - complexity) * 0.3f)
                        AddFrontiers(mid);
                }
            }

            // replace frontier marks
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (maze[x, y] < 0) maze[x, y] = 0;

            return maze;
        }

        private void PlaceExits(int count, int height, int width)
        {
            _exits.Clear();
            System.Random rnd = new();
            int attempts = 0;

            while (_exits.Count < count && attempts < 1000)
            {
                attempts++;
                bool horizontal = rnd.Next(2) == 0;
                int x = horizontal ? (rnd.Next(2) == 0 ? 0 : width - 1) : rnd.Next(1, width - 2);
                int y = horizontal ? rnd.Next(1, height - 2) : (rnd.Next(2) == 0 ? 0 : height - 1);

                Vector2Int exitPos = new(x, y);
                if (_exits.Exists(e => Vector2Int.Distance(e, exitPos) < 2f))
                    continue;

                Vector2Int inward = new(
                    x == 0 ? 1 : (x == width - 1 ? -1 : 0),
                    y == 0 ? 1 : (y == height - 1 ? -1 : 0)
                );

                // look inward up to 2 cells to find corridor
                bool foundPassage = false;
                Vector2Int inside = exitPos;
                for (int i = 1; i <= 2; i++)
                {
                    Vector2Int check = exitPos + inward * i;
                    if (check.x >= 0 && check.y >= 0 && check.x < width && check.y < height && _maze[check.x, check.y] == 1)
                    {
                        inside = check;
                        foundPassage = true;
                        break;
                    }
                }
                if (!foundPassage) continue;

                _maze[inside.x - inward.x, inside.y - inward.y] = 1;
                _maze[exitPos.x, exitPos.y] = 1;
                _exits.Add(exitPos);
            }
        }

        private static bool InBounds(Vector2Int pos, int width, int height)
            => pos.x > 0 && pos.y > 0 && pos.x < width - 1 && pos.y < height - 1;

        private static readonly Vector2Int[] Directions =
        {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };
    }
}
