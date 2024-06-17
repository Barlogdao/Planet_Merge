using PlanetMerge.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI.View
{
    [RequireComponent(typeof(MovingBackground), typeof(RawImage))]
    public class SpaceBackground : MonoBehaviour
    {
        private const string StrongTintFade = "_StrongTintFade";

        [SerializeField] private Color[] _colors;
        [SerializeField] private float _fadeDuration;

        private MovingBackground _background;
        private Material _backgroundMaterial;
        private RawImage _image;
        private ShaderFadeTween _fadeTween;

        private void Awake()
        {
            _image = GetComponent<RawImage>();
            _backgroundMaterial = _image.material;
            _background = GetComponent<MovingBackground>();
            _fadeTween = new ShaderFadeTween(_backgroundMaterial, _fadeDuration, StrongTintFade);
        }

        public void Run()
        {
            _background.enabled = true;
            _image.color = GetColor();
            _fadeTween.Fade();
        }

        public void Stop()
        {
            _background.enabled = false;
        }

        private Color GetColor()
        {
            Color color;

            do
            {
                color = _colors[Random.Range(0, _colors.Length)];
            }
            while (color == _image.color);

            return color;
        }
    }
}