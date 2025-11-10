using Fps.MVP.Components;
using Fps.MVP.Model;
using VContainer;

namespace Components
{
    public class Component<TModel> : Component 
        where TModel : Model
    {
        [Inject]
        protected TModel Model { get; private set; }
        
    }
}