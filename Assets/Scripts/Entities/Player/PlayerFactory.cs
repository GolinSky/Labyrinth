using Mvp.Repository;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.Entities.Player
{
    public class PlayerFactory
    {
        private readonly LifetimeScope _rootScope;
        
        public PlayerFactory(IObjectResolver resolver)
        {
            _rootScope = resolver.Resolve<LifetimeScope>();
        }
        
        public void CreatePlayer(Vector3 startPosition)
        {
            IRepository repository = _rootScope.Container.Resolve<IRepository>();
            PlayerLifetimeScope scopePrefab = repository.LoadComponent<PlayerLifetimeScope>(nameof(PlayerLifetimeScope));

            _rootScope.CreateChildFromPrefab(scopePrefab, builder =>
            {
                builder.RegisterInstance(startPosition);
            });
        }
    }
}