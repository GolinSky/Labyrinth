using Maze.Services.Game;
using Maze.Ui;
using Mvp.Presenter;
using Mvp.Services;
using VContainer.Unity;

namespace Maze.Entities.MainMenu
{
    public interface IMainMenuPresenter : IPresenter
    {
        void StartGame();
        void ExitGame();
    }
    
    public class MainMenuService: Service, IStartable, IMainMenuPresenter
    {
        private readonly IUiService _uiService;
        private readonly IGameService _gameService;
        private MainMenuUi _mainMenuUi;
        
        
        public MainMenuService(IUiService uiService, IGameService gameService) 
        {
            _uiService = uiService;
            _gameService = gameService;
        }

        public void Start()
        {
            _mainMenuUi = _uiService.CreateUi<MainMenuUi>();
            _mainMenuUi.AssignPresenter(this);
            _mainMenuUi.Show();
        }

        public void StartGame()
        {
            _gameService.StartGame();
        }

        public void ExitGame()
        {
            _gameService.ExitGame();
        }
    }
}