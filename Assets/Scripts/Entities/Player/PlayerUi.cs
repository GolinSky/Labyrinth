using Maze.Ui;
using TMPro;
using UnityEngine;

namespace Maze.Entities.Player
{
    public class PlayerUi: ModelBasedUi<IPlayerModelObserver>
    {
        [SerializeField] protected TextMeshProUGUI stepsText;
        [SerializeField] protected TextMeshProUGUI timeText;
        
        public void UpdateSteps()
        {
            stepsText.text = $"Steps: {Model.Steps}";
        }
        
        public void UpdateTime()
        {
            timeText.text = $"Time: {Model.TimeSpent:F2}s";
        }
    }
}