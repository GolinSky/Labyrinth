using System;
using System.Collections.Generic;

namespace Maze.Services.Score
{
    [Serializable]
    public class ScoreData
    {
         public float MatchDuration { get; set; }
         public int StepsAmount { get; set; }
         public string Timestamp { get; set; }
    }
    
    [Serializable]
    public class ScoresData
    {
        public List<ScoreData> Scores { get; set; } = new List<ScoreData>();

        public void AddScore(ScoreData scoreData)
        {
            Scores ??= new List<ScoreData>();
            Scores.Add(scoreData);
        }
    }
}