using PlanetMerge.Configs;
using PlanetMerge.Systems;
using TMPro;
using UnityEngine;

namespace PlanetMerge.UI
{
    public class GoalBar : PlanetViewBar
    {
        [SerializeField] private TMP_Text _mergeAmountLabel;
        [SerializeField] private PlanetViewService _planetViewService;

        private LevelGoalHandler _levelGoalHandler;
        public void Initialize(LevelGoalHandler levelGoalHandler)
        {
            _levelGoalHandler = levelGoalHandler;

            _levelGoalHandler.GoalChanged += OnGoalChanged;
        }

        private void OnDestroy()
        {
            _levelGoalHandler.GoalChanged -= OnGoalChanged;
        }

        private void OnGoalChanged(int amount)
        {
            _mergeAmountLabel.text = amount.ToString();
        }
    }
}