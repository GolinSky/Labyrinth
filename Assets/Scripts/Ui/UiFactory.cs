using System;
using Fps.MVP.Model;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;
using Exception = System.Exception;

namespace Fps.Ui
{
    public class UiFactory
    {
        private readonly LifetimeScope _rootScope;
        
        public UiFactory(IObjectResolver resolver)
        {
            _rootScope = resolver.Resolve<LifetimeScope>();
        }
        
        
        public TUi CreateUi<TUi>(Transform parent)
            where TUi : BaseUi
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
            where TUi : BaseUi
            where TModel : IModelObserver
        {
            var prefab = Addressables.LoadAssetAsync<GameObject>(typeof(TUi).Name).WaitForCompletion();
            if (prefab == null)
                throw new Exception($"UI prefab for {typeof(TUi).Name} not found in Addressables.");

            var instance = UnityEngine.Object.Instantiate(prefab, parent);
            instance.name = $"{typeof(TUi).Name}_Instance";

            var childScope = _rootScope.CreateChild(builder =>
            {
                builder.RegisterComponentInHierarchy<TUi>().AsImplementedInterfaces(); // injects components inside prefab
                builder.RegisterInstance(model);
     
            });

            
            var ui = instance.GetComponent<TUi>();

            return ui;
        }
    }
}