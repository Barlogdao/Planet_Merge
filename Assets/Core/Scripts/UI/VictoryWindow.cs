using Cysharp.Threading.Tasks;
using PlanetMerge.Utils;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI
{
    public class VictoryWindow : AppearWindow
    {
        [SerializeField] private PlanetProgressionPanel _progressionPanel;
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private ScoreTween _scoreTween;

        //public void Initialize()
        //{
        //    _progressionPanel.Initialize();
        //}

        private void Awake()
        {
            _progressionPanel.Initialize();
        }

        public async UniTask ShowAsync(int levelScore, int currentPlanetRank, IReadOnlyPlayerData playerData)
        {
            AppearAsync().Forget();

            _progressionPanel.Prepare(currentPlanetRank);
            _scoreLabel.text = levelScore.ToString();
            int playerScore = playerData.Score;


            await UniTask.WhenAll(
                _progressionPanel.ShowProgressAsync(playerData),
                _scoreTween.Run(levelScore, playerScore, _scoreLabel));
        }
    }
}