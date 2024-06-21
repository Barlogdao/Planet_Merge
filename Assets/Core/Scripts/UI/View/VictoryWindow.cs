using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.Data;
using PlanetMerge.Utils.Tweens;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI.View
{
    public class VictoryWindow : AppearWindow
    {
        [SerializeField] private PlanetProgressionPanel _progressionPanel;
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private ScoreTween _scoreTween;

        public async UniTask ShowAsync(int levelScore, int currentPlanetRank, IReadOnlyPlayerData playerData)
        {
            AppearAsync().Forget();

            int newScore = playerData.Score;
            int previousScore = newScore - levelScore;

            _progressionPanel.Prepare(currentPlanetRank);
            _scoreLabel.text = newScore.ToString();

            await UniTask.WhenAll(
                _progressionPanel.ShowProgressAsync(playerData),
                _scoreTween.RunAsync(previousScore, newScore, _scoreLabel));
        }
    }
}