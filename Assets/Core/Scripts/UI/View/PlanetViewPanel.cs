using DG.Tweening;
using PlanetMerge.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI.View
{
    public abstract class PlanetViewPanel : MonoBehaviour
    {
        [SerializeField] private Image _planetImage;
        [SerializeField] private TMP_Text _planetRankLabel;
        [SerializeField] private PlanetViewService _planetViewService;

        [SerializeField] private float _tweenDuration = 0.2f;
        [SerializeField] private Ease _ease;

        private Vector2 _startScale = Vector2.zero;
        private Vector2 _originScale;

        private void Awake()
        {
            _originScale = _planetImage.transform.localScale;
        }

        public void Prepare(int planetRank)
        {
            PlanetViewData planetViewData = _planetViewService.GetViewData(planetRank);

            _planetImage.sprite = planetViewData.Sprite;
            _planetRankLabel.text = planetViewData.RankText;
            _planetRankLabel.color = planetViewData.LabelColor;

            _planetImage.transform
                .DOScale(_startScale, _tweenDuration)
                .From()
                .OnComplete(() => { _planetImage.transform.localScale = _originScale; });
        }
    }
}