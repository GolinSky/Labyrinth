using Fps.MVP.Services;
using Maze.Services.Scenes;
using UnityEngine;
using VContainer.Unity;

namespace Fps.Services.Game
{
    public interface IGameService: IService
    {
        void StartGame();
    }
    
    public class GameService: Service, IInitializable, IGameService
    {
        private readonly ISceneService _sceneService;

        public GameService(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }

        public void Initialize()
        {
            // _sceneService.LoadScene(SceneType.MainMenu);
        }

        public void StartGame()
        {
            _sceneService.LoadScene(SceneType.Core);
        }
    }
}