using Fps.MVP.Repository;
using Fps.Services.Coroutines;
using Fps.Services.Game;
using Fps.Services.Repository;
using Maze.Services.Scenes;
using Maze.VContainer.Utility;
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



            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GameService>();
            });
            
        }
    }
}