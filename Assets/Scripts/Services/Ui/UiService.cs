using Mvp.Model;
using Mvp.Presenter;
using UnityEngine;
using VContainer;

namespace Maze.Ui
{
    public interface IUiService
    {
      //  TUI CreateUi<TUI>(Transform uiParent = null) where TUI : IBaseUi;
        TUI CreateUi<TUI, TModel>(TModel model, Transform uiParent = null)
            where TUI : IBaseUi
            where TModel : IModelObserver;

        TUI CreatePresenterBasedUi<TUI, TPresenter>(TPresenter presenter, Transform uiParent = null)
            where TUI : IBaseUi
            where TPresenter : IPresenter;
    }
    
    public class UiService: MonoBehaviour, IUiService
    {
        [SerializeField] private Transform parent;
        
        private readonly Transform _uiRoot;
        
        private UiFactory UIFactory { get; set; }

        [Inject]
        private void Construct(UiFactory uiFactory)
        {
            UIFactory = uiFactory;
        }

        //todo: refactor - inject presenter as model in overload method
        public TUI CreateUi<TUI>(Transform uiParent = null) where TUI : IBaseUi
        {
            TUI uiInstance = UIFactory.CreateUi<TUI>(uiParent ?? this.parent);
            return uiInstance;
        }

        public TUI CreateUi<TUI, TModel>(TModel model, Transform uiParent = null)
            where TUI : IBaseUi
            where TModel : IModelObserver
        {
            TUI uiInstance = UIFactory.CreateUi<TUI, TModel>(uiParent ?? this.parent, model);
            return uiInstance;
        }
        
        public TUI CreatePresenterBasedUi<TUI, TPresenter>(TPresenter presenter, Transform uiParent = null)
            where TUI : IBaseUi
            where TPresenter : IPresenter
        {
            TUI uiInstance = UIFactory.CreateUiWithPresenter<TUI, TPresenter>(uiParent ?? this.parent, presenter);
            return uiInstance;
        }
    }
}