using Fps.MVP.Repository;
using Maze.VContainer.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.Entities.Player
{
    public class PlayerLifetimeScope: LifetimeScope
    {
        private IObjectResolver _resolver;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _resolver = Parent.Container;
            IRepository repository = _resolver.Resolve<IRepository>();
            // Vector3 startPosition = _resolver.Resolve<Vector3>();
            // builder.RegisterInstance(startPosition);
            
            builder.RegisterScriptableObject<PlayerData>();
            builder.Register<PlayerModel>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();

            PlayerView prefab = repository.LoadComponent<PlayerView>(nameof(PlayerView));
            builder.RegisterComponentInNewPrefab(prefab, Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            
            builder.Register<PlayerPresenter>(Lifetime.Scoped);

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<PlayerPresenter>();
            });
        }
    }
}