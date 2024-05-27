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

        private PlanetLimitHandler _planetLimitHandler;

        public void Initialize(PlanetLimitHandler planetLimitHandler)
        {
            _planetLimitHandler = planetLimitHandler;

            _planetLimitHandler.LimitChanged += OnLimitChanged;
            _scaleTween.Initialize(_limitAmount.transform);
        }

        private void OnDestroy()
        {
            _planetLimitHandler.LimitChanged += OnLimitChanged;
        }

        private void OnLimitChanged(int amount)
        {
            _limitAmount.text = amount.ToString();
            _scaleTween.Run();
        }
    }
}