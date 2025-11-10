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

        void IStartable.Start()
        {
            playButton.onClick.AddListener(StartGame);
        }
        
        
        public void Dispose()
        {
            playButton.onClick.RemoveListener(StartGame);
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
    }
}