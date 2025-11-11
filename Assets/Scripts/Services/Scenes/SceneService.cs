using Mvp.Services;
using UnityEngine.SceneManagement;

namespace Maze.Services.Scenes
{
    public interface ISceneService : IService
    {
        void LoadScene(SceneType sceneType);
    }

    public class SceneService : Service, ISceneService //, IInitializable, ILateDisposable
    {
        private readonly SceneModel _sceneModel;
        public SceneType TargetScene { get; private set; }

        public SceneService(SceneModel sceneModel)
        {
            _sceneModel = sceneModel;
        }

        public void LoadScene(SceneType sceneType)
        {
            TargetScene = sceneType;
            SceneManager.LoadScene(_sceneModel.GetScene(sceneType).SceneName, LoadSceneMode.Single);
        }
    }
}