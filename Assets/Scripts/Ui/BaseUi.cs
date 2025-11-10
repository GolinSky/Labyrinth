using Mvp.Presenter;
using UnityEngine;

namespace Maze.Ui
{
    public interface IBaseUi
    {
        void Show();
        void Hide();
    }
    
    public abstract class BaseUi:MonoBehaviour, IBaseUi
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        
        public void Show()
        {
            SetCanvasState(true);
        }

        public void Hide()
        {
            SetCanvasState(false);
        }
        
        private void SetCanvasState(bool state)
        {
            canvasGroup.alpha = state ? 1f : 0f;
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
        }
    }
    

    public abstract class BaseUi<TPresenter> : BaseUi
        where TPresenter : IPresenter
    {
       protected TPresenter Presenter { get; private set; }


       public void AssignPresenter(TPresenter presenter)
       {
           Presenter = presenter;
       }
    }
}