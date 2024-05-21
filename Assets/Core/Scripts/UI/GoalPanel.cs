using PlanetMerge.Configs;
using PlanetMerge.Systems;
using PlanetMerge.Utils;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI
{
    public class GoalPanel : PlanetViewPanel
    {
        [SerializeField] private TMP_Text _mergeAmountLabel;
        [SerializeField] private ScaleTween _scaleTween;

        private LevelGoalHandler _levelGoalHandler;
        public void Initialize(LevelGoalHandler levelGoalHandler)
        {
            _levelGoalHandler = levelGoalHandler;

            _levelGoalHandler.GoalChanged += OnGoalChanged;
            _scaleTween.Initialize(_mergeAmountLabel.transform);
        }

        private void OnDestroy()
        {
            _levelGoalHandler.GoalChanged -= OnGoalChanged;
        }

        private void OnGoalChanged(int amount)
        {
            _mergeAmountLabel.text = amount.ToString();
            _scaleTween.Run();
        }
    }
}