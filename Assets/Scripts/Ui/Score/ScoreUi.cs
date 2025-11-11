using System;
using System.Collections.Generic;
using Maze.Services.MainMenu;
using Maze.Services.Score;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Maze.Ui.Score
{
    public class ScoreUi: PresenterBasedUi<IScorePresenter>, IInitializable, IDisposable
    {
        [SerializeField] private Transform scoreParent;
        [SerializeField] private ScoreFieldUi scoreFieldPrefab;
        [SerializeField] private Button closeButton;
        
        private readonly List<ScoreFieldUi> _scoreFieldInstances = new();
        
        public void Initialize()
        {
            closeButton.onClick.AddListener(Presenter.CloseScores);
        }

        public void Dispose()
        {
            closeButton.onClick.RemoveListener(Presenter.CloseScores);
        }
        
        public void SetData(IReadOnlyList<ScoreData> scoresData)
        {
            foreach (ScoreFieldUi scoreFieldUi in _scoreFieldInstances)
            {
                Destroy(scoreFieldUi.gameObject);
            }
            _scoreFieldInstances.Clear();
            
            int index = 0;
            foreach (ScoreData scoreData in scoresData)
            {
                index++;
                ScoreFieldUi scoreFieldUi = Instantiate(scoreFieldPrefab, scoreParent);
                scoreFieldUi.SetData(index,scoreData);
                _scoreFieldInstances.Add(scoreFieldUi);
            }
        }
    }
}