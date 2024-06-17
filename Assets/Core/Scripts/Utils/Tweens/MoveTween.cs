using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PlanetMerge.Utils
{
    [System.Serializable]
    public class MoveTween
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Ease _ease;
        [SerializeField] private Vector2 _targetPosition;

        public async UniTask RunAsync(Transform transform)
        {
            await transform.DOMove(_targetPosition, _duration).SetEase(_ease);
        }
    }
}