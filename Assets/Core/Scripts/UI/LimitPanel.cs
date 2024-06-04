using DG.Tweening;
using PlanetMerge.Configs;
using PlanetMerge.Systems;
using PlanetMerge.Utils;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI
{
    public class LimitPanel:MonoBehaviour
    {
        [SerializeField] private TMP_Text _limitAmount;
        [SerializeField] private ScaleTween _scaleTween;

        private EnergyLimitHandler _energyLimitHandler;

        public void Initialize(EnergyLimitHandler energyLimitHandler)
        {
            _energyLimitHandler = energyLimitHandler;

            _energyLimitHandler.LimitChanged += OnLimitChanged;
            _scaleTween.Initialize(_limitAmount.transform);
        }

        private void OnDestroy()
        {
            _energyLimitHandler.LimitChanged += OnLimitChanged;
        }

        private void OnLimitChanged(int amount)
        {
            _limitAmount.text = amount.ToString();
            _scaleTween.Run();
        }
    }
}