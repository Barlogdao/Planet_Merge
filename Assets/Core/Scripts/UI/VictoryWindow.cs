using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI
{
    public class VictoryWindow : UiWindow
    {
        [SerializeField] private PlanetProgressionPanel _progressionPanel;
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private float _scoreTweenDuration = 1f;
        [SerializeField] private Ease _ease;

        private int _currentScore;

        public void Initialize()
        {
            _progressionPanel.Initialize();
        }

        public void Prepare(IReadOnlyPlayerData playerData)
        {
            _progressionPanel.Prepare(playerData.PlanetRank);
            _scoreLabel.text = playerData.Score.ToString();

            _currentScore = playerData.Score;
        }

        public void ShowProgress(IReadOnlyPlayerData playerData)
        {
            _progressionPanel.ShowProgress(playerData).Forget();

            int targetScore = playerData.Score;

            DOVirtual.Int(_currentScore, targetScore, _scoreTweenDuration, OnScoreChanged);
        }

        private void OnScoreChanged(int value)
        {
            _scoreLabel.text = value.ToString();
        }
    }
}