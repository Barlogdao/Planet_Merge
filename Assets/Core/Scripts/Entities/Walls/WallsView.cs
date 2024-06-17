using Cysharp.Threading.Tasks;
using UnityEngine;
using PlanetMerge.Utils;
using System.Collections.Generic;

namespace PlanetMerge.Entities.Walls
{
    public class WallsView : AppearingEntity
    {
        private const string FadeProperty = "_FullGlowDissolveFade";

        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private float _fadeDuration = 0.14f;

        private List<ShaderFadeTween> _fadeTweens;

        public override async UniTask AppearAsync()
        {
            foreach (ShaderFadeTween fadeTween in _fadeTweens)
            {
                await fadeTween.UnfadeAsync();
            }
        }

        public override async UniTask DisappearAsync()
        {
            foreach (ShaderFadeTween fadeTween in _fadeTweens)
            {
                await fadeTween.FadeAsync();
            }
        }

        protected override void OnAwake()
        {
            _fadeTweens = new List<ShaderFadeTween>();

            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            {
                Material material = spriteRenderer.material;
                _fadeTweens.Add(new ShaderFadeTween(material, _fadeDuration, FadeProperty));
            }
        }

        protected override void OnResetView()
        {
            foreach (ShaderFadeTween fadeTween in _fadeTweens)
            {
                fadeTween.SetFaded();
            }
        }
    }
}