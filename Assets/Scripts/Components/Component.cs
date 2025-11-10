using Mvp.Components;
using Mvp.Model;
using VContainer;

namespace Maze.Components
{
    public class Component<TModel> : Component 
        where TModel : Model
    {
        [Inject]
        protected TModel Model { get; private set; }
        
    }
}