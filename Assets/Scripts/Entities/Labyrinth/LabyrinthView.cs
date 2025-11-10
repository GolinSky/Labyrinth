using System.Collections.Generic;
using Maze.View;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maze.Entities.Labyrinth
{
    public class LabyrinthView : View<ILabyrinthModelObserver, ILabyrinthPresenter>
    {
        [Header("Tilemap")] 
        [SerializeField] private Tilemap wallMap;
        [SerializeField] private Tilemap floorMap;
        [SerializeField] private TileBase wallTile;
        [SerializeField] private TileBase floorTile;
        [SerializeField] private TileBase exitTile;
        
        public void DrawMaze(int[,] mazeMatrix, List<Vector2Int> exits)
        {
            wallMap.ClearAllTiles();
            floorMap.ClearAllTiles();

            for (int x = 0; x < Model.Width; x++)
            {
                for (int y = 0; y < Model.Height; y++)
                {
                    Vector3Int pos = new(x, y, 0);

                    if (mazeMatrix[x, y] == 1)
                    {
                        floorMap.SetTile(pos, floorTile);
                    }
                    else
                    {
                        wallMap.SetTile(pos, wallTile);
                    }
                }
            }
            
            foreach (var exit in exits)
            {
                floorMap.SetTile(new Vector3Int(exit.x, exit.y, 0), exitTile);
            }
        }
    }
}