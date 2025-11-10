using Fps.MVP.Model;
using Fps.MVP.Presenter;
using Fps.MVP.Views;
using UnityEngine;
using UnityEngine.Assertions;

namespace Maze.View
{
    public abstract class View<TModel, TPresenter> : MonoBehaviour, IView
        where TPresenter : class, IPresenter
        where TModel : IModelObserver
    {
        public Transform Transform => transform;
        
        protected TPresenter Presenter { get; private set; }

        public IModelObserver ModelObserver => null;// Model;
        
        // [Inject]
        // protected TModel Model { get; private set; }


        public void AssignPresenter(IPresenter presenter)
        {
            Assert.IsTrue(presenter is TPresenter);
            Presenter = (TPresenter)presenter;
        }

    }
}