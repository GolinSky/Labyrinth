using Maze.VContainer.Factory;
using UnityEngine;
using VContainer;

namespace Maze.Entities.Player
{
    public sealed class PlayerFactory: BaseFactory
    {
        public PlayerFactory(IObjectResolver resolver): base(resolver)
        {
        }
        
        public void CreatePlayer(Vector3 startPosition)
        {
            PlayerLifetimeScope scopePrefab = Repository.LoadComponent<PlayerLifetimeScope>(nameof(PlayerLifetimeScope));

            RootScope.CreateChildFromPrefab(scopePrefab, builder =>
            {
                builder.RegisterInstance(startPosition);
            });
        }
    }
}