using Fps.MVP.Presenter;
using Fps.MVP.Services;
using Fps.Services.Game;
using Fps.Ui;
using VContainer.Unity;

namespace Maze.Entities.MainMenu
{
    public interface IMainMenuPresenter : IPresenter
    {
        void StartGame();
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
    }
}