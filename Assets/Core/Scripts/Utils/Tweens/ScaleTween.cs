using DG.Tweening;
using UnityEngine;

namespace PlanetMerge.Utils
{
    [System.Serializable]
    public class ScaleTween
    {
        [SerializeField] private float _scaleMiltiplier = 1.4f;
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private Ease _ease = Ease.OutSine;

        private Transform _targetTransform;
        private Vector3 _originScale;

        public void Initialize(Transform transform)
        {
            _targetTransform = transform;
            _originScale = transform.localScale;
        }

        public void Run()
        {
            _targetTransform
                .DOScale(_scaleMiltiplier, _duration)
                .SetEase(_ease)
                .OnComplete(() => _targetTransform.localScale = _originScale);
        }
    }
}