using Mvp.Model;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;
using Exception = System.Exception;

namespace Maze.Ui
{
    public class UiFactory
    {
        private readonly LifetimeScope _rootScope;
        
        public UiFactory(IObjectResolver resolver)
        {
            _rootScope = resolver.Resolve<LifetimeScope>();
        }
        
        
        public TUi CreateUi<TUi>(Transform parent)
            where TUi : IBaseUi
        {
            var prefab = Addressables.LoadAssetAsync<GameObject>(typeof(TUi).Name).WaitForCompletion();
            if (prefab == null)
                throw new Exception($"UI prefab for {typeof(TUi).Name} not found in Addressables.");

            var instance = UnityEngine.Object.Instantiate(prefab, parent);
            instance.name = $"{typeof(TUi).Name}_Instance";

            var childScope = _rootScope.CreateChild(builder =>
            {
                builder.RegisterComponentInHierarchy<TUi>().AsImplementedInterfaces(); // injects components inside prefab
            });


            var ui = instance.GetComponent<TUi>();
          //  var ui = childScope.Container.Resolve<TUi>();
            return ui;
        }
        
        
        
        public TUi CreateUi<TUi, TModel>(Transform parent, TModel model) 
            where TUi : IBaseUi
            where TModel : IModelObserver
        {
            var prefab = Addressables.LoadAssetAsync<GameObject>(typeof(TUi).Name).WaitForCompletion();
            if (prefab == null)
                throw new Exception($"UI prefab for {typeof(TUi).Name} not found in Addressables.");

            var instance = Object.Instantiate(prefab, parent);
            instance.name = $"{typeof(TUi).Name}_Instance";

            var ui = instance.GetComponent<TUi>();
            if (ui == null)
                throw new Exception($"Prefab {typeof(TUi).Name} does not contain a component of type {typeof(TUi).Name}.");
            
            var childScope = _rootScope.CreateChild(builder =>
            {
                builder.RegisterComponentInHierarchy<TUi>().AsImplementedInterfaces(); // injects components inside prefab
                builder.RegisterInstance(model);
     
            });

            
          
            

            return ui;
        }
    }
}