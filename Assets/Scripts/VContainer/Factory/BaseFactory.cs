using Mvp.Repository;
using VContainer;
using VContainer.Unity;

namespace Maze.VContainer.Factory
{
    public abstract class BaseFactory
    {
        protected IRepository Repository => RootScope.Container.Resolve<IRepository>();

        protected LifetimeScope RootScope { get; }

        protected BaseFactory(IObjectResolver resolver)
        {
            RootScope = resolver.Resolve<LifetimeScope>();
        }
    }
}