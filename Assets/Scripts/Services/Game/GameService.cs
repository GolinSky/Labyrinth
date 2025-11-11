using Maze.Services.Scenes;
using Mvp.Services;
using UnityEngine;

namespace Maze.Services.Game
{
    public interface IGameService: IService
    {
        void StartGame();
        void ExitGame();
    }
    
    public class GameService: Service, IGameService
    {
        private readonly ISceneService _sceneService;

        public GameService(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }

        public void StartGame()
        {
            _sceneService.LoadScene(SceneType.Core);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}