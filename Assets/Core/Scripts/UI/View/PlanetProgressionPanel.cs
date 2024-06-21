using Cysharp.Threading.Tasks;
using DG.Tweening;
using PlanetMerge.Systems.Data;
using PlanetMerge.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI.View
{
    public class PlanetProgressionPanel : PlanetViewPanel
    {
        private readonly int _upgradeStep = Constants.PlanetUpgradeStep;
        private readonly float _minSliderVolume = 0f;
        private readonly float _maxSliderVolume = 1f;

        [SerializeField] private Slider _slider;
        [SerializeField] private float _fillDuration;
        [SerializeField] private Ease _fillEase;

        public async UniTask ShowProgressAsync(IReadOnlyPlayerData playerData)
        {
            int level = playerData.Level;
            int previousLevel = playerData.Level - 1;
            float sliderValue = GetSliderValue(previousLevel);

            _slider.value = sliderValue;

            if (IsNeedUpgradePlanet(level))
                sliderValue = _maxSliderVolume;
            else
                sliderValue = GetSliderValue(level);

            await _slider.DOValue(sliderValue, _fillDuration);

            if (IsNeedUpgradePlanet(level))
            {
                Prepare(playerData.PlanetRank);
                _slider.value = _minSliderVolume;
            }
        }

        private bool IsNeedUpgradePlanet(int level)
        {
            return level % _upgradeStep == 0;
        }

        private float GetSliderValue(int level)
        {
            return level % _upgradeStep / (float)_upgradeStep;
        }
    }
}