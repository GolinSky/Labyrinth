using Maze.VContainer.Factory;
using VContainer;

namespace Maze.Entities.Player
{
    public sealed class PlayerFactory: BaseFactory
    {
        public PlayerFactory(IObjectResolver resolver): base(resolver)
        {
        }
        
        public void CreatePlayer()
        {
            PlayerLifetimeScope scopePrefab = Repository.LoadComponent<PlayerLifetimeScope>(nameof(PlayerLifetimeScope));
            RootScope.CreateChildFromPrefab(scopePrefab);
        }
    }
}