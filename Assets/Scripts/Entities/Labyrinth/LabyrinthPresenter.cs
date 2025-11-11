using Maze.Services.Labyrinth;
using Mvp.Presenter;
using UnityEngine;
using VContainer.Unity;

namespace Maze.Entities.Labyrinth
{
    public interface ILabyrinthPresenter : IPresenter
    {
        
    }
    
    public class LabyrinthPresenter: Presenter<LabyrinthModel, LabyrinthView>, ILabyrinthPresenter, IInitializable, ILabyrinthProvider//, ITickable
    {
        private readonly ILabyrinthGenerator _labyrinthGenerator;
        private GeneratedLabyrinthData _generatedLabyrinthData;

        public int Width => Model.Width;
        public int Height => Model.Height;
        
        public LabyrinthPresenter(
            LabyrinthModel model,
            LabyrinthView view,
            ILabyrinthGenerator labyrinthGenerator) : base(model, view)
        {
            _labyrinthGenerator = labyrinthGenerator;
        }

        public void Initialize()
        {
            _generatedLabyrinthData = _labyrinthGenerator.GenerateMaze(
                Model.Width,
                Model.Height,
                Model.Complexity,
                Model.Density, 
                Model.ExitCount);
            
            View.DrawMaze(_generatedLabyrinthData.Maze, _generatedLabyrinthData.Exits);
        }
        
        public bool IsExit(Vector2Int cell)
        {
            return _generatedLabyrinthData.Exits.Contains(cell);
        }

        public Vector3 GetNearestWalkableCell(Vector2Int from)
        {
            Vector2Int currentCell = FindNearestFloor(from);

            Vector3 cellCenter = View.GetCellCenterWorld(new Vector3Int(currentCell.x, currentCell.y, 0));
            return cellCenter;
        }
        
        public Vector3 GetWorldCoordinates(Vector2Int from)
        {
            Vector3 worldCoordinates = View.GetCellCenterWorld(new Vector3Int(from.x, from.y, 0));
            return worldCoordinates;
        }
        

        public bool IsWalkable(Vector2Int cell)
        {
            if (cell.x < 0 || cell.y < 0 || cell.x >= Model.Width || cell.y >= Model.Height) return false;
            return _generatedLabyrinthData.Maze[cell.x, cell.y] == 1;
        }
        
        public Vector2Int FindNearestFloor(Vector2Int center)
        {
            if (IsWalkable(center))
                return center;

            int maxRadius = Mathf.Max(Model.Width, Model.Height);
            for (int r = 1; r < maxRadius; r++)
            {
                for (int dx = -r; dx <= r; dx++)
                for (int dy = -r; dy <= r; dy++)
                {
                    Vector2Int check = new(center.x + dx, center.y + dy);
                    if (IsWalkable(check))
                        return check;
                }
            }

            return center;
        }

        // public void Tick()
        // {
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         _generatedLabyrinthData = _labyrinthGenerator.GenerateMaze(
        //             Model.Width,
        //             Model.Height,
        //             Model.Complexity,
        //             Model.Density, 
        //             Model.ExitCount);
        //     
        //         View.DrawMaze(_generatedLabyrinthData.Maze, _generatedLabyrinthData.Exits);
        //     }
        // }
    }
}