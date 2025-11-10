using System.Collections.Generic;
using UnityEngine;

namespace Maze.Entities.Labyrinth
{
    public interface ILabyrinthGenerator
    {
        //todo: GenerateMaze must return maze data { maze, exits }
        int[,] Maze { get; }
        List<Vector2Int> Exits { get; }

        void GenerateMaze(int width, int height, float complexity, float density, int exitCount);
    }
    public class LabyrinthGenerator:ILabyrinthGenerator
    {
        public int[,] Maze { get; private set; }
        public List<Vector2Int> Exits { get; private set; } = new();
        

        public void GenerateMaze(int width, int height, float complexity, float density, int exitCount)
        {
            Maze = GenerateMazeInternal(width, height, complexity, density);
            PlaceExits(exitCount, height: height, width: width);
        }
        
        private int[,] GenerateMazeInternal(int width, int height, float complexity, float density)
        {
            int[,] maze = new int[width, height];

            // Ensure odd dimensions for perfect maze
            if (width % 2 == 0) width--;
            if (height % 2 == 0) height--;

            Stack<Vector2Int> stack = new();
            Vector2Int start = new(width / 2, height / 2);
            maze[start.x, start.y] = 1;
            stack.Push(start);

            Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            System.Random rnd = new();

            int maxCells = (int)(width * height * density);
            int carvedCells = 1;

            while (stack.Count > 0 && carvedCells < maxCells)
            {
                var current = stack.Peek();
                var possibleDirs = new List<Vector2Int>();

                foreach (var d in dirs)
                {
                    Vector2Int next = current + d * 2;
                    if (next.x > 0 && next.y > 0 && next.x < width - 1 && next.y < height - 1 && maze[next.x, next.y] == 0)
                        possibleDirs.Add(d);
                }

                if (possibleDirs.Count > 0)
                {
                    float branchChance = 1f - complexity; // Higher complexity = fewer branches
                    if (rnd.NextDouble() < branchChance)
                    {
                        // randomly pop stack to add open branches
                        stack.Pop();
                        if (stack.Count == 0)
                            stack.Push(current);
                    }

                    var dir = possibleDirs[rnd.Next(possibleDirs.Count)];
                    maze[current.x + dir.x, current.y + dir.y] = 1;
                    maze[current.x + dir.x * 2, current.y + dir.y * 2] = 1;
                    carvedCells += 2;
                    stack.Push(current + dir * 2);
                }
                else
                {
                    stack.Pop();
                }
            }

            return maze;
        }
        
        private void PlaceExits(int count, int height, int width)
        {
            Exits.Clear();
            System.Random rnd = new();

            HashSet<Vector2Int> usedPositions = new();

            int attempts = 0;
            while (Exits.Count < count && attempts < 1000)
            {
                attempts++;

                bool horizontal = rnd.Next(2) == 0;
                int x = horizontal ? (rnd.Next(2) == 0 ? 0 : width - 1) : rnd.Next(1, width - 2);
                int y = horizontal ? rnd.Next(1, height - 2) : (rnd.Next(2) == 0 ? 0 : height - 1);

                Vector2Int exitPos = new(x, y);

                // Ensure not overlapping or adjacent to another exit
                bool tooClose = false;
                foreach (var e in Exits)
                {
                    if (Vector2Int.Distance(e, exitPos) < 2f)
                    {
                        tooClose = true;
                        break;
                    }
                }
                if (tooClose) continue;

                // Check that adjacent inside cell is walkable
                Vector2Int inward = new(
                    x == 0 ? 1 : (x == width - 1 ? -1 : 0),
                    y == 0 ? 1 : (y == height - 1 ? -1 : 0)
                );

                Vector2Int inside = exitPos + inward;
                if (inside.x < 0 || inside.y < 0 || inside.x >= width || inside.y >= height) continue;
                if (Maze[inside.x, inside.y] != 1) continue; // ensure open passage

                Maze[exitPos.x, exitPos.y] = 1; // make sure exit cell is walkable
                Exits.Add(exitPos);
                usedPositions.Add(exitPos);
            }
        }
    }
}