using System;
using Maze.Services.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Maze.Ui.EndGame
{
    public class EndGameUi: PresenterBasedUi<ICoreGamePresenter>,IInitializable, IDisposable
    {
        [SerializeField] private TextMeshProUGUI stepsText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        

        public void Dispose()
        {
            restartButton.onClick.RemoveListener(Presenter.RestartGame);
            exitButton.onClick.RemoveListener(Presenter.ExitGame);
        }
        
        public void SetData(int steps, float timeSpent)
        {
            stepsText.text = $"Steps: {steps}";
            timeText.text = $"Time: {timeSpent:F2}s";
        }

        public void Initialize()
        {
            restartButton.onClick.AddListener(Presenter.RestartGame);
            exitButton.onClick.AddListener(Presenter.ExitGame);
        }
    }
}