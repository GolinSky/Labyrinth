using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.Entities.Labyrinth
{
    public class LabyrinthLifeTimeScope: LifetimeScope
    {
        [SerializeField] private LabyrinthView labyrinthView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MazeGenerationStrategy>(Lifetime.Scoped).As<ILabyrinthGenerator>();
            builder.Register<LabyrinthModel>(Lifetime.Scoped).AsSelf().As<ILabyrinthModelObserver>();
            builder.RegisterComponent(labyrinthView).AsSelf().AsImplementedInterfaces();
            builder.Register<LabyrinthPresenter>(Lifetime.Scoped);

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<LabyrinthPresenter>();
            });
        }
    }
}