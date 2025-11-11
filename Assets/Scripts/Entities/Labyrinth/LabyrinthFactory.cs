using Maze.Services.Labyrinth;
using Maze.VContainer.Factory;
using Maze.VContainer.Utility;
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
            var scope = RootScope.CreateChildFromPrefab(scopePrefab, builder =>
            {
                builder.RegisterScriptableObject<LabyrinthData>().As<ILabyrinthContext>().As<IGenerationContext>();
   
            });
            return scope.Container.Resolve<ILabyrinthProvider>();
        }

        public ILabyrinthProvider CreateLabyrinth(ILabyrinthContext labyrinthContext)
        {
            LabyrinthLifeTimeScope scopePrefab = Repository.LoadComponent<LabyrinthLifeTimeScope>(nameof(LabyrinthLifeTimeScope));
            var scope = RootScope.CreateChildFromPrefab(scopePrefab, builder =>
            {
                builder.RegisterInstance(labyrinthContext);
                builder.RegisterScriptableObject<LabyrinthData>().As<IGenerationContext>();
            });
            return scope.Container.Resolve<ILabyrinthProvider>();
        }
    }
}