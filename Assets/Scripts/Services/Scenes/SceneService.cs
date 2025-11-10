using System;
using Maze.Services.Coroutines;
using Mvp.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze.Services.Scenes
{
    public interface ISceneService //: IService
    {
        void LoadScene(SceneType sceneType);
        SceneType TargetScene { get; }

    }

    public class SceneService : Service, ISceneService//, IInitializable, ILateDisposable
    {
        private readonly SceneModel _sceneModel;
        private readonly ICoroutineService _coroutineService;
        private const float MIN_SCENE_PROGRESS = 0.88f;
        public event Action<SceneType> OnSceneActivation;


        private AsyncOperation _asyncOperation;

        public SceneType TargetScene { get; private set; }



        public SceneService(SceneModel sceneModel, ICoroutineService coroutineService)
        {
            _sceneModel = sceneModel;
            _coroutineService = coroutineService;
        }
        

        public void LoadScene(SceneType sceneType)
        {
            TargetScene = sceneType;
            
           SceneManager.LoadScene(_sceneModel.GetScene(sceneType).SceneName, LoadSceneMode.Single);
        }
    }
}