using System.Collections.Generic;
using Maze.Services.Save;
using Maze.Services.Score;
using Mvp.Services;

public class ScoreService : Service
{
    private const string SCORE_FILE = "score.json";

    private readonly SaveService _saveService;
    private readonly ScoresData _scoresData;

    public ScoreService(SaveService saveService)
    {
        _saveService = saveService;
        _scoresData = _saveService.Load<ScoresData>(SCORE_FILE) ?? new ScoresData();
    }

    public void SaveScore(float matchDuration, int steps)
    {
        ScoreData scoreData = new ScoreData
        {
            MatchDuration = matchDuration,
            StepsAmount = steps,
            Timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        _scoresData.AddScore(scoreData);
        _saveService.Save(SCORE_FILE, _scoresData);
    }

    public IReadOnlyList<ScoreData> GetAllScores() => _scoresData.Scores;
}