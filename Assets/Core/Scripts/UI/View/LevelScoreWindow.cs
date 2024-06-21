using Cysharp.Threading.Tasks;
using PlanetMerge.Utils.Tweens;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI.View
{
    public class LevelScoreWindow : AppearWindow
    {
        private readonly int _startScore = 0;

        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private ScoreTween _scoreTween;
        [SerializeField] private MoveTween _moveTween;

        private Vector3 _originPosition;

        private void Awake()
        {
            _originPosition = transform.position;
            Hide();
        }

        public async UniTask ShowScoreAsync(int score)
        {
            _scoreLabel.text = _startScore.ToString();

            await AppearAsync();
            await _scoreTween.RunAsync(_startScore, score, _scoreLabel);
            await _moveTween.RunAsync(transform);

            transform.position = _originPosition;
            Hide();
        }
    }
}