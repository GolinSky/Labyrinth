using Fps.Services.Game;
using Maze.Entities.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.VContainer
{
    public class CoreLifeTimeScope: LifetimeScope
    {
      
        [SerializeField] private Camera mainCamera;
       
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CoreGameService>(Lifetime.Singleton);
            // builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();
           
            builder.RegisterInstance(mainCamera);
            
            // builder.RegisterFactory<PlayerView>(() =>
            // {
            //     var repository = Container.Resolve<IRepository>();
            //     var prefab = repository.LoadComponent<PlayerLifetimeScope>(nameof(PlayerView));
            //     var childScope = CreateChildFromPrefab(prefab);
            //     return childScope.Container.Resolve<PlayerView>(); // Execute per factory invocation
            // });
            //

            builder.Register<PlayerFactory>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            
            builder.UseEntryPoints(Lifetime.Singleton, pointsBuilder =>
            {
                pointsBuilder.Add<CoreGameService>();
            });
        }
    }
}