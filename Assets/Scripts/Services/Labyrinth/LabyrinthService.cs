using Maze.Entities.Labyrinth;
using Mvp.Services;
using UnityEngine;

namespace Maze.Services.Labyrinth
{
    public interface ILabyrinthProvider
    {
        bool IsWalkable(Vector2Int cell);
        bool IsExit(Vector2Int cell);
        Vector3 GetNearestWalkableCell(Vector2Int from);
        int Width { get; }
        int Height { get; }
    }
    
    public interface ILabyrinthService: IService,ILabyrinthProvider
    {
      //set data from ui - dynamic data - width height exits count etc...
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
            _labyrinthProvider = labyrinthFactory.CreateLabyrinth();
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