using DG.Tweening;
using UnityEngine;

namespace PlanetMerge.Entities.Wall
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Wall : MonoBehaviour
    {
        private const string FadeProperty = "_AddColorFade";

        [SerializeField] private float _fadeDuration = 0.2f;

        private Material _material;
        private int _fadePropertyID;
        private float _maxFadeValue = 1f;
        private float _minFadeValue = 0f;

        void Start()
        {
            _material = GetComponent<SpriteRenderer>().material;

            _fadePropertyID = Shader.PropertyToID(FadeProperty);

            _material.SetFloat(_fadePropertyID, _minFadeValue);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _material.SetFloat(_fadePropertyID, _maxFadeValue);
            _material.DOFloat(_minFadeValue, _fadePropertyID, _fadeDuration);
        }
    }
}