using Maze.Services.Coroutines;
using Maze.Services.Game;
using Maze.Services.Repository;
using Maze.Services.Save;
using Maze.Services.Scenes;
using Maze.VContainer.Utility;
using Mvp.Repository;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.VContainer
{
    public class ProjectLifeTimeScope: LifetimeScope
    {
        [SerializeField] private CoroutineService coroutineService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneService>(Lifetime.Singleton).As<ISceneService>();
            builder.RegisterScriptableObject<SceneData>();

            builder.Register<SceneModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.RegisterInstance(coroutineService).As<ICoroutineService>();
            builder.Register<GameService>(Lifetime.Singleton);
            builder.Register<AddressableRepository>(Lifetime.Singleton).As<IRepository>();
            builder.Register<SaveService>(Lifetime.Singleton);
            builder.Register<ScoreService>(Lifetime.Singleton);


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GameService>();
            });
            
        }
    }
}