using Maze.Services.MainMenu;
using VContainer;
using VContainer.Unity;

namespace Maze.VContainer
{
    public class MainMenuLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MainMenuService>(Lifetime.Scoped);
            
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<MainMenuService>();
            });
        }
    }
}