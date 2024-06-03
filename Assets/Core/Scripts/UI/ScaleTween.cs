using DG.Tweening;
using UnityEngine;

namespace PlanetMerge.Utils
{
    public class ScaleTween : MonoBehaviour
    {
        [SerializeField] private float _scaleMiltiplier;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private Transform _targetTransform;
        private Vector3 _originScale;
        public void Initialize(Transform transform)
        {
            _targetTransform = transform;
            _originScale = transform.localScale;
        }

        public void Run()
        {
            _targetTransform.DOScale(_scaleMiltiplier, _duration).SetEase(_ease).OnComplete(() => _targetTransform.localScale = _originScale);
        }
    }
}