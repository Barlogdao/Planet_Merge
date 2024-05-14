using PlanetMerge.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI
{
    public class PlanetViewBar : MonoBehaviour
    {
        [SerializeField] private Image _planetImage;
        [SerializeField] private TMP_Text _planetRankLabel;
        [SerializeField] private PlanetViewService _planetViewService;
        
        public void Prepare(int planetRank)
        {
            PlanetViewData planetViewData = _planetViewService.GetViewData(planetRank);

            _planetImage.sprite = planetViewData.Sprite;
            _planetRankLabel.text = planetViewData.RankText;
            _planetRankLabel.color = planetViewData.LabelColor;
        }
    }
}