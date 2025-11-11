using Maze.VContainer.Factory;
using Mvp.Model;
using Mvp.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Exception = System.Exception;

namespace Maze.Ui
{
    public class UiFactory: BaseFactory
    {
        public UiFactory(IObjectResolver resolver): base(resolver)
        {
        }
        
        public TUi CreateUiWithPresenter<TUi, TPresenter>(Transform parent, TPresenter presenter)
            where TUi : IBaseUi
            where TPresenter : IPresenter
        {
            var uiInstance = CreateUiInstance<TUi>(parent);

            var childScope = RootScope.CreateChild(builder =>
            {
                builder.RegisterComponentInHierarchy<TUi>().AsImplementedInterfaces(); 
                builder.RegisterInstance(presenter);
            });

            return uiInstance;
        }
        
        public TUi CreateUi<TUi>(Transform parent)
            where TUi : IBaseUi
        {
            var uiInstance = CreateUiInstance<TUi>(parent);

            var childScope = RootScope.CreateChild(builder =>
            {
                builder.RegisterComponentInHierarchy<TUi>().AsImplementedInterfaces(); 
            });

            return uiInstance;
        }
        
        public TUi CreateUi<TUi, TModel>(Transform parent, TModel model) 
            where TUi : IBaseUi
            where TModel : IModelObserver
        {
            var uiInstance = CreateUiInstance<TUi>(parent);
            
            var childScope = RootScope.CreateChild(builder =>
            {
                builder.RegisterComponentInHierarchy<TUi>().AsImplementedInterfaces(); // injects components inside prefab
                builder.RegisterInstance(model);
     
            });
            return uiInstance;
        }

        private TUi CreateUiInstance<TUi>(Transform parent)
        {
            var prefab = Repository.Load<GameObject>(typeof(TUi).Name);
            if (prefab == null)
                throw new Exception($"UI prefab for {typeof(TUi).Name} not found in Addressables.");

            var instance = Object.Instantiate(prefab, parent);
            instance.name = $"{typeof(TUi).Name}_Instance";

            var ui = instance.GetComponent<TUi>();
            if (ui == null)
                throw new Exception($"Prefab {typeof(TUi).Name} does not contain a component of type {typeof(TUi).Name}.");

            return ui;
        }
    }
}