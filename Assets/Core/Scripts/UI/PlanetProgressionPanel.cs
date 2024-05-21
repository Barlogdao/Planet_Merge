using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI
{
    public class PlanetProgressionPanel : PlanetViewPanel
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _fillDuration;
        [SerializeField] private Ease _fillEase;

        private int _upgradeStep;
        private float _minSliderVolume;
        private float _maxSliderVolume;
        public void Initialize()
        {
            _upgradeStep = Constants.PlanetUpgradeStep;
            _minSliderVolume = _slider.minValue;
            _maxSliderVolume = _slider.maxValue;
        }

        public async UniTaskVoid Set(IReadOnlyPlayerData playerData)
        {
            int level = playerData.Level;
            int previousLevel = playerData.Level - 1;
            float sliderValue = GetSliderValue(previousLevel);

            _slider.value = sliderValue;

            await UniTask.WaitForSeconds(_fillDuration);

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

        private bool IsNeedUpgradePlanet (int level)
        {
            return level%_upgradeStep == 0;
        }

        private float GetSliderValue(int level)
        {
            return level % _upgradeStep / (float)_upgradeStep;
        }
    }
}