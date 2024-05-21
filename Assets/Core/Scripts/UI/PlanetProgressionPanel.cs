using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI
{
    public class PlanetProgressionPanel : PlanetViewPanel
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _fillSpeed;
        [SerializeField] private int _upgradeStep;

        public async UniTaskVoid Set(IReadOnlyPlayerData playerData)
        {
            int currentLevel = playerData.Level;
            int previousLevel = playerData.Level - 1;

            if (currentLevel % _upgradeStep == 0)
                Prepare(playerData.PlanetRank - 1);
            else
                Prepare(playerData.PlanetRank);

            _slider.value = (previousLevel) % _upgradeStep / (float)_upgradeStep;


            await _slider.DOValue(currentLevel % _upgradeStep / (float)_upgradeStep, _fillSpeed);

            if (playerData.Level % _upgradeStep == 0)
            {
                Prepare(playerData.PlanetRank);
                _slider.value = 0f;
            }
        }
    }
}