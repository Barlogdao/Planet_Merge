using Cysharp.Threading.Tasks;
using DG.Tweening;
using PlanetMerge.Utils.Extensions;
using UnityEngine;

namespace PlanetMerge.UI.View
{
    [RequireComponent(typeof(CanvasGroup), (typeof(RectTransform)))]
    public class UiPanelView : AppearingEntity
    {
        private const float Zero = 0f;

        [SerializeField] private float _tweenDuration = 1f;
        [SerializeField] private float _targetScale = 1.3f;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _middlePosition = Vector3.zero;
        [SerializeField] private Ease _ease;

        private readonly float _minAlpha = 0f;
        private readonly float _maxAlpha = 1f;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private Vector3 _originScale;
        private Vector3 _originAnchoredPosition;

        protected override void OnAwake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _originScale = _rectTransform.localScale;
            _originAnchoredPosition = _rectTransform.anchoredPosition;
        }

        protected override void OnResetView()
        {
            _rectTransform.position = _startPosition;
            _rectTransform.localScale = _originScale;
            _canvasGroup.alpha = _maxAlpha;
        }

        public override async UniTask AppearAsync()
        {
            ResetPositionZ();

            await UniTask.WhenAll(
                _rectTransform.DOMove(_middlePosition, _tweenDuration).SetEase(_ease).ToUniTask(),
                _rectTransform.DOScale(_targetScale, _tweenDuration).SetEase(_ease).ToUniTask());

            await UniTask.WhenAll(
                _rectTransform.DOScale(_originScale, _tweenDuration).SetEase(_ease).ToUniTask(),
                _rectTransform.DOAnchorPos(_originAnchoredPosition, _tweenDuration).SetEase(_ease).ToUniTask());

            ResetPositionZ();
        }

        public override async UniTask DisappearAsync()
        {
            await _canvasGroup.DOFade(_minAlpha, _tweenDuration);
        }

        private void ResetPositionZ()
        {
            _rectTransform.localPosition = _rectTransform.localPosition.WithZ(Zero);
        }
    }
}