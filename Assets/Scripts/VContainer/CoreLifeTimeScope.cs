using Maze.Entities.Labyrinth;
using Maze.Entities.Player;
using Maze.Services.CameraService;
using Maze.Services.Game;
using Maze.Services.Labyrinth;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.VContainer
{
    public class CoreLifeTimeScope: LifetimeScope
    {
        [SerializeField] private CameraService cameraService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // builder.Register<CoreGameService>(Lifetime.Singleton).AsImplementedInterfaces();
            // builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterComponent(cameraService).As<ICameraService>();

            builder.Register<LabyrinthFactory>(Lifetime.Scoped);
            builder.Register<LabyrinthService>(Lifetime.Singleton).AsSelf().As<ILabyrinthService>();
            
    

            builder.Register<PlayerFactory>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            
            builder.UseEntryPoints(Lifetime.Singleton, pointsBuilder =>
            {
                pointsBuilder.Add<CoreGameService>();
            });
        }
    }
}