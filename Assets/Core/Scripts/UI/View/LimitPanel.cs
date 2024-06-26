using PlanetMerge.Systems.Gameplay;
using PlanetMerge.Utils.Tweens;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI.View
{
    public class LimitPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _limitAmount;
        [SerializeField] private ScaleTween _scaleTween;

        private IEnergyLimitNotifier _energyLimitNotifier;

        private void OnDestroy()
        {
            _energyLimitNotifier.LimitChanged += OnLimitChanged;
        }

        public void Initialize(IEnergyLimitNotifier energyLimitNotifier)
        {
            _energyLimitNotifier = energyLimitNotifier;

            _energyLimitNotifier.LimitChanged += OnLimitChanged;
            _scaleTween.Initialize(_limitAmount.transform);
        }

        private void OnLimitChanged(int amount)
        {
            _limitAmount.text = amount.ToString();
            _scaleTween.Run();
        }
    }
}