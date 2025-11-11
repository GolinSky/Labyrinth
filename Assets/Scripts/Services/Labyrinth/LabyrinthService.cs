using Maze.Entities.Labyrinth;
using Mvp.Services;
using UnityEngine;

namespace Maze.Services.Labyrinth
{
    public interface ILabyrinthService : IService, ILabyrinthProvider
    {
        void ConstructMaze(ILabyrinthContext labyrinthContext);
    }
    
    public class LabyrinthService: Service, ILabyrinthService
    {
        private readonly LabyrinthFactory _labyrinthFactory;
        private ILabyrinthProvider _labyrinthProvider;
        
        public int Width => _labyrinthProvider.Width;
        public int Height => _labyrinthProvider.Height;
        
        public LabyrinthService(LabyrinthFactory labyrinthFactory)
        {
            _labyrinthFactory = labyrinthFactory;
        }
 
        public void ConstructMaze(ILabyrinthContext labyrinthContext)
        {
            _labyrinthProvider = _labyrinthFactory.CreateLabyrinth(labyrinthContext);
        }
        
        public Vector2Int FindNearestFloor(Vector2Int center)
        {
            return _labyrinthProvider.FindNearestFloor(center);
        }

        public Vector3 GetWorldCoordinates(Vector2Int from)
        {
            return _labyrinthProvider.GetWorldCoordinates(from);
        }

        public bool IsWalkable(Vector2Int cell)
        {
           return _labyrinthProvider.IsWalkable(cell);
        }

        public bool IsExit(Vector2Int cell)
        {
            return _labyrinthProvider.IsExit(cell);
        }

        public Vector3 GetNearestWalkableCell(Vector2Int from)
        {
            return _labyrinthProvider.GetNearestWalkableCell(from);
        }
    }
}