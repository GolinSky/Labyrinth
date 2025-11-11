using System;
using Maze.Services.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Maze.Ui.EndGame
{
    public class EndGameUi: BaseUi<ICoreGamePresenter>, IDisposable
    {
        [SerializeField] private TextMeshProUGUI stepsText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        
        protected override void OnPresenterAssigned()
        {
            restartButton.onClick.AddListener(Presenter.RestartGame);
            exitButton.onClick.AddListener(Presenter.ExitGame);
        }

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
    }
}