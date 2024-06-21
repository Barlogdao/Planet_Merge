using PlanetMerge.Utils.Tweens;
using UnityEngine;

namespace PlanetMerge.Entities.Walls
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Wall : MonoBehaviour
    {
        private const string FadeProperty = "_AddColorFade";

        [SerializeField] private float _fadeDuration = 0.2f;

        private ShaderFadeTween _fadeTween;
        private Material _material;

        void Start()
        {
            _material = GetComponent<SpriteRenderer>().material;
            _fadeTween = new ShaderFadeTween(_material, _fadeDuration, FadeProperty);
            _fadeTween.SetFaded();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _fadeTween.Fade();
        }
    }
}