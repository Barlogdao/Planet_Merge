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

        private SpriteRenderer _spriteRenderer;
        private Vector3 _originScale;

        private void Awake()
        {
            _originScale = transform.localScale;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Set(int rank)
        {
            PlanetViewData data = _viewProvider.GetViewData(rank);


            _rankLabel.text = data.RankText;
            _rankLabel.color = data.LabelColor;
            _spriteRenderer.sprite = data.Sprite;
        }

        public void Collide()
        {
            transform.DOShakeScale(_shakeDuration, _shakeStrength).OnComplete(() => transform.localScale = _originScale);
        }

        public void Show()
        {
            _spriteRenderer.enabled = true;
            _rankLabel.enabled = true;
        }
        public void Hide()
        {
            _spriteRenderer.enabled = false;
            _rankLabel.enabled = false;

        }
    }
}