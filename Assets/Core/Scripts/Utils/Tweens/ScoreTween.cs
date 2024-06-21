using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PlanetMerge.Utils.Tweens
{
    [System.Serializable]
    public class ScoreTween
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Ease _ease;

        public async UniTask RunAsync(int startValue, int endValue, TMP_Text scoreLabel)
        {
            await DOVirtual.Int(startValue, endValue, _duration, UpdateScore).SetEase(_ease); ;

            void UpdateScore(int value)
            {
                scoreLabel.text = value.ToString();
            }
        }
    }
}