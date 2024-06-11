using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PlanetMerge.Utils
{
    public class ShaderFadeTween
    {
        private readonly float _fadeValue = 0f;
        private readonly float _unfadeValue = 1f;
        private readonly Material _material;
        private readonly float _duration;
        private readonly int _propertyID;

        public ShaderFadeTween(Material material, float duration, string property)
        {
            _material = material;
            _duration = duration;
            _propertyID = Shader.PropertyToID(property);
        }

        public void Run()
        {
            _material.SetFloat(_propertyID, _unfadeValue);
            _material.DOFloat(_fadeValue, _propertyID, _duration);
        }

        public async UniTask RunAsync()
        {
            _material.SetFloat(_propertyID, _unfadeValue);
            await _material.DOFloat(_fadeValue, _propertyID, _duration);
        }
    }
}