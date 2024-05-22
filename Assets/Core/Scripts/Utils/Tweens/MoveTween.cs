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

        public async UniTask Run(Transform transform, Vector3 targetPosition, bool isFromPosition = false)
        {
            if (isFromPosition)
            {
                await transform.DOMove(targetPosition, _duration).From().SetEase(_ease);

            }
            else
            {
                await transform.DOMove(targetPosition, _duration).SetEase(_ease);
            }
        }
    }
}