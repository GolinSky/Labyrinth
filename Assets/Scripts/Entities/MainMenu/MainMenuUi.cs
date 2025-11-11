using System;
using Maze.Ui;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Maze.Entities.MainMenu
{
    public class MainMenuUi: BaseUi<IMainMenuPresenter>, IStartable, IDisposable
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        void IStartable.Start()
        {
            playButton.onClick.AddListener(StartGame);
            playButton.onClick.AddListener(ExitGame);
        }

        public void Dispose()
        {
            playButton.onClick.RemoveListener(StartGame);
            playButton.onClick.RemoveListener(ExitGame);
        }

        private void StartGame()
        {
            if (Presenter == null)
            {
                Debug.LogWarning($"Presenter is null in {nameof(MainMenuUi)}");
            }
            else
            {
                Presenter.StartGame();
            }
        }
        
        private void ExitGame()
        {
            if (Presenter == null)
            {
                Debug.LogWarning($"Presenter is null in {nameof(MainMenuUi)}");
            }
            else
            {
                Presenter.ExitGame();
            }
        }
    }
}