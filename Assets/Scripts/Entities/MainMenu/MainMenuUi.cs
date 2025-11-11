using System;
using Maze.Ui;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Maze.Entities.MainMenu
{
    public class MainMenuUi: PresenterBasedUi<IMainMenuPresenter>, IStartable, IDisposable
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        void IStartable.Start()
        {
            playButton.onClick.AddListener(Presenter.StartGame);
            exitButton.onClick.AddListener(Presenter.ExitGame);
        }

        public void Dispose()
        {
            playButton.onClick.RemoveListener(Presenter.StartGame);
            exitButton.onClick.RemoveListener(Presenter.ExitGame);
        }
    }
}