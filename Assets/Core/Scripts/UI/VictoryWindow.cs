using Cysharp.Threading.Tasks;
using DG.Tweening;
using PlanetMerge.Utils;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI
{
    public class VictoryWindow : UiWindow
    {
        [SerializeField] private PlanetProgressionPanel _progressionPanel;
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private ScoreTween _scoreTween;

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

            _scoreTween.Run(_currentScore, targetScore, _scoreLabel).Forget();
        }
    }
}