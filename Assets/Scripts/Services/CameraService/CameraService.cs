using Maze.Services.Labyrinth;
using Mvp.Services;
using UnityEngine;

namespace Maze.Services.CameraService
{
    public interface ICameraService: IService
    {
        void SetUpCamera(ILabyrinthService labyrinthService);
    }
    
    public class CameraService: MonoBehaviour, ICameraService
    {
        private const float MARGIN = 1f;
        private const float Z_OFFSET = -10f;
        
        [SerializeField] private Camera targetCamera;

        public void SetUpCamera(ILabyrinthService labyrinthService)
        {
            // make one method api from labyrinth service 
            float centerX = (labyrinthService.Width - 1) / 2f;
            float centerY = (labyrinthService.Height - 1) / 2f;

            Vector3 mazeCenter = labyrinthService.GetWorldCoordinates(new Vector2Int((int)centerX, (int)centerY));
            mazeCenter.z = Z_OFFSET;
            targetCamera.transform.position = mazeCenter;
            
            float aspect = (float)Screen.width / Screen.height;
            float halfWidth = labyrinthService.Width / 2f;
            float halfHeight = labyrinthService.Height / 2f;

            targetCamera.orthographicSize = Mathf.Max(halfHeight, halfWidth / aspect) + MARGIN; 
        }
    }
}