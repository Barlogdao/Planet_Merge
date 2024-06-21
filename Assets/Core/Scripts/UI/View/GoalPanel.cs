using PlanetMerge.Systems.Gameplay;
using PlanetMerge.Utils.Tweens;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI.View
{
    public class GoalPanel : PlanetViewPanel
    {
        [SerializeField] private TMP_Text _mergeAmountLabel;
        [SerializeField] private ScaleTween _scaleTween;

        private ILevelGoalNotifier _levelGoalNotifier;

        public void Initialize(ILevelGoalNotifier levelGoalNotifier)
        {
            _levelGoalNotifier = levelGoalNotifier;

            _levelGoalNotifier.GoalChanged += OnGoalChanged;
            _scaleTween.Initialize(_mergeAmountLabel.transform);
        }

        private void OnDestroy()
        {
            _levelGoalNotifier.GoalChanged -= OnGoalChanged;
        }

        private void OnGoalChanged(int amount)
        {
            _mergeAmountLabel.text = amount.ToString();
            _scaleTween.Run();
        }
    }
}