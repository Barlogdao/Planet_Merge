using PlanetMerge.Configs;
using PlanetMerge.Systems;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI
{
    public class LimitBar : PlanetViewBar
    {
        [SerializeField] private TMP_Text _limitAmount;
        [SerializeField] private PlanetViewService _planetViewService;

        private PlanetLimitHandler _planetLimitHandler;

        public void Initialize(PlanetLimitHandler planetLimitHandler)
        {
            _planetLimitHandler = planetLimitHandler;

            _planetLimitHandler.LimitChanged += OnLimitChanged;
        }

        private void OnDestroy()
        {
            _planetLimitHandler.LimitChanged += OnLimitChanged;
        }

        private void OnLimitChanged(int amount)
        {
            _limitAmount.text = amount.ToString();
        }

    }
}