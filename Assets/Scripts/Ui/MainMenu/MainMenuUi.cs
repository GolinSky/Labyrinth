using System;
using Maze.Ui;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Maze.Services.MainMenu
{
    public class MainMenuUi: PresenterBasedUi<IMainMenuPresenter>, IStartable, IDisposable
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button scoresButton;

        void IStartable.Start()
        {
            playButton.onClick.AddListener(Presenter.StartGame);
            exitButton.onClick.AddListener(Presenter.ExitGame);
            scoresButton.onClick.AddListener(Presenter.ShowScores);
        }

        public void Dispose()
        {
            playButton.onClick.RemoveListener(Presenter.StartGame);
            exitButton.onClick.RemoveListener(Presenter.ExitGame);
        }
    }
}