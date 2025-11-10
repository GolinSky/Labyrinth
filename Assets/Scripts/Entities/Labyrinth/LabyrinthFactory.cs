using Maze.Services.Labyrinth;
using Maze.VContainer.Factory;
using VContainer;

namespace Maze.Entities.Labyrinth
{
    public class LabyrinthFactory: BaseFactory
    {
        public LabyrinthFactory(IObjectResolver resolver) : base(resolver)
        {
        }
        
        public ILabyrinthProvider CreateLabyrinth()
        {
            LabyrinthLifeTimeScope scopePrefab = Repository.LoadComponent<LabyrinthLifeTimeScope>(nameof(LabyrinthLifeTimeScope));
            var scope = RootScope.CreateChildFromPrefab(scopePrefab);
            return scope.Container.Resolve<ILabyrinthProvider>();
        }
    }
}