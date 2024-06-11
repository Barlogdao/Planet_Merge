using Cysharp.Threading.Tasks;
using DG.Tweening;
using PlanetMerge.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI.View
{
    [System.Serializable]
    public class FadingBackground
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private Ease _ease = Ease.Linear;

        private float _minAlphaValue = 0f;
        private float _maxAlphaValue = 1f;

        public void Initialize()
        {
            _maxAlphaValue = _image.color.a;
            Hide();
        }

        public void Show()
        {
            _image.color = _image.color.WithAlpha(_maxAlphaValue);
        }

        public void Hide()
        {
            _image.color = _image.color.WithAlpha(_minAlphaValue);
        }

        public void Fade()
        {
            _image.DOFade(_minAlphaValue, _fadeDuration).SetEase(_ease);
        }

        public async UniTask FadeAsync()
        {
            await _image.DOFade(_minAlphaValue, _fadeDuration).SetEase(_ease);
        }

        public void Update()
        {
            _image.DOFade(_maxAlphaValue, _fadeDuration).SetEase(_ease);
        }

        public async UniTask UnfadeAsync()
        {
            await _image.DOFade(_maxAlphaValue, _fadeDuration).SetEase(_ease);
        }
    }
}