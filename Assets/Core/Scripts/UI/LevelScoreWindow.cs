using Cysharp.Threading.Tasks;
using PlanetMerge.Utils;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI
{
    public class LevelScoreWindow : UiWindow
    {
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private ScoreTween _scoreTween;
        [SerializeField] private MoveTween _moveTween;

        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;

        private int _startScore = 0;
        private Vector3 _originPosition;

        private void Awake()
        {
            _originPosition = transform.position;
        }

        public async UniTask Animate(int score)
        {
            _scoreLabel.text = _startScore.ToString();

            await _moveTween.Run(transform, _startPosition, true);
            await _scoreTween.Run(_startScore, score, _scoreLabel);
            await _moveTween.Run(transform, _endPosition, false);

            transform.position = _originPosition;
            Hide();
        }
    }
}