using Maze.Ui;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.VContainer
{
    public class SceneLifeTimeScope : LifetimeScope
    {
        [SerializeField] private UiService uiService;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<UiFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.RegisterComponent(uiService).As<IUiService>();

        }
    }
}