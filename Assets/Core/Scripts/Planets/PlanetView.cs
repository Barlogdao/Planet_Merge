using DG.Tweening;
using PlanetMerge.Configs;
using TMPro;
using UnityEngine;

namespace PlanetMerge.Planets
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlanetView : MonoBehaviour
    {
        [SerializeField] private PlanetViewService _viewProvider;
        [SerializeField] private TMP_Text _rankLabel;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrength;

        private Planet _planet;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _originScale;


        public void Initialize(Planet planet)
        {
            _planet = planet;
            _originScale = transform.localScale;
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _planet.Collided += OnCollide;
            _planet.Merged += OnMerge;
        }

        private void OnMerge(Planet planet)
        {
            Set(planet.Rank);
        }

        public void Set(int rank)
        {
            PlanetViewData data = _viewProvider.GetViewData(rank);


            _rankLabel.text = data.RankText;
            _rankLabel.color = data.LabelColor;
            _spriteRenderer.sprite = data.Sprite;
        }

        private void OnCollide(Vector2 atPoint)
        {
            transform.DOShakeScale(_shakeDuration, _shakeStrength).OnComplete(()=> transform.localScale = _originScale);
        }

        private void OnDestroy()
        {
            _planet.Collided -= OnCollide;
            _planet.Merged -= OnMerge;
        }
    }
}