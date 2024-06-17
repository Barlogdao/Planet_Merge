using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PlanetMerge.Utils
{
    public class ShaderFadeTween
    {
        private readonly float _fadeValue = 0f;
        private readonly float _unfadeValue = 1f;
        private readonly float _duration;
        private readonly int _propertyID;
        private readonly Material _material;

        public ShaderFadeTween(Material material, float duration, string property)
        {
            _material = material;
            _duration = duration;
            _propertyID = Shader.PropertyToID(property);
        }

        public void SetFaded()
        {
            _material.SetFloat(_propertyID, _fadeValue);
        }

        public void SetUnfaded()
        {
            _material.SetFloat(_propertyID, _unfadeValue);
        }

        public void Fade()
        {
            SetUnfaded();
            _material.DOFloat(_fadeValue, _propertyID, _duration);
        }

        public void Unfade()
        {
            SetFaded();
            _material.DOFloat(_unfadeValue, _propertyID, _duration);
        }

        public async UniTask FadeAsync()
        {
            SetUnfaded();
            await _material.DOFloat(_fadeValue, _propertyID, _duration);
        }

        public async UniTask UnfadeAsync()
        {
            SetFaded();
            await _material.DOFloat(_unfadeValue, _propertyID, _duration);
        }
    }
}