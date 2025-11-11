using Maze.Services.Score;
using TMPro;
using UnityEngine;

namespace Maze.Services.MainMenu
{
    public class ScoreFieldUi: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private TextMeshProUGUI stepsText;
        [SerializeField] private TextMeshProUGUI timeText;

        public void SetData(int index, ScoreData scoreData)
        {
            numberText.text = $"#{index}";
            stepsText.text = $"Steps: {scoreData.StepsAmount}";
            timeText.text = $"Time: {scoreData.MatchDuration:F2}";
        }
    }
}