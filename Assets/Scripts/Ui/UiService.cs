using System;
using UnityEngine;
using VContainer;

namespace Maze.Ui
{
    public interface IUiService
    {
        TUI CreateUi<TUI>(Transform parent = null) where TUI : BaseUi;
    }
    
    public class UiService: MonoBehaviour, IUiService
    {
        private readonly Func<Transform, BaseUi> _factory;

        [Inject]
        private IObjectResolver _resolver;

        [SerializeField] private Transform parent;


        private readonly IObjectResolver _rootResolver;
        private readonly Transform _uiRoot;


        private UiFactory UIFactory { get; set; }

        [Inject]
        private void Construct(UiFactory uiFactory)
        {
            UIFactory = uiFactory;
        }

        public TUI CreateUi<TUI>(Transform parent = null) where TUI : BaseUi
        {
            TUI uiInstance = UIFactory.CreateUi<TUI>( parent ?? this.parent);
            return uiInstance;
        }
    }
}