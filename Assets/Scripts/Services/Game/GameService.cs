using Maze.Services.Scenes;
using Mvp.Services;
using UnityEngine;

namespace Maze.Services.Game
{
    public interface IGameService: IService
    {
        void StartGame();
        void ExitGame();
        void RestartCoreGame();
        void LoadMenu();
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
            LoadCoreGame();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void RestartCoreGame()
        {
            LoadCoreGame();
        }

        public void LoadMenu()
        {
            _sceneService.LoadScene(SceneType.MainMenu);
        }

        private void LoadCoreGame()
        {
            _sceneService.LoadScene(SceneType.Core);
        }
    }
}