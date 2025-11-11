using Maze.Services.Game;
using Maze.Services.Score;
using Maze.Ui;
using Maze.Ui.Score;
using Mvp.Presenter;
using Mvp.Services;
using VContainer.Unity;

namespace Maze.Services.MainMenu
{
    public interface IMainMenuPresenter : IPresenter
    {
        void StartGame();
        void ExitGame();
        void ShowScores();
    }
    
    public class MainMenuService: Service, IStartable, IMainMenuPresenter,IScorePresenter
    {
        private readonly IUiService _uiService;
        private readonly IGameService _gameService;
        private ScoreService _scoreService;
        private MainMenuUi _mainMenuUi;
        private ScoreUi _scoreUi;

        public MainMenuService(IUiService uiService, IGameService gameService, ScoreService scoreService) 
        {
            _scoreService = scoreService;
            _uiService = uiService;
            _gameService = gameService;
        }

        public void Start()
        {
            _mainMenuUi = _uiService.CreatePresenterBasedUi<MainMenuUi, IMainMenuPresenter>(this);
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

        public void ShowScores()
        {
            if (_scoreUi == null)
            {
                _scoreUi = _uiService.CreatePresenterBasedUi<ScoreUi, IScorePresenter>(this);
            }
            _scoreUi.SetData(_scoreService.GetScores());
            _scoreUi.Show();
            _mainMenuUi.Hide();
        }

        public void CloseScores()
        {
            _scoreUi.Hide();
            _mainMenuUi.Show();
        }
    }
}