using Maze.Services.Game;
using TMPro;
using UnityEngine;

namespace Maze.Ui.EndGame
{
    public class EndGameUi: BaseUi<ICoreGamePresenter>
    {
        [SerializeField] protected TextMeshProUGUI stepsText;
        [SerializeField] protected TextMeshProUGUI timeText;
        
        public void SetData(int steps, float timeSpent)
        {
            stepsText.text = $"Steps: {steps}";
            timeText.text = $"Time: {timeSpent:F2}s";
        }
    }
}