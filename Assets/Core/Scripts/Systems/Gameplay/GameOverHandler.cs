using System;
using UnityEngine;

namespace PlanetMerge.Systems.Gameplay
{
    public class GameOverHandler : MonoBehaviour
    {
        private IEnergyLimitNotifier _energyLimitNotifier;
        private ILevelGoalNotifier _levelGoalNotifier;

        public event Action GameWon;

        public event Action GameLost;

        private void OnDestroy()
        {
            _energyLimitNotifier.LimitExpired -= OnLimitExpired;
            _levelGoalNotifier.GoalReached -= OnGoalReached;
        }

        public void Initialize(IEnergyLimitNotifier energyLimitNotifier, ILevelGoalNotifier levelGoalNotifier)
        {
            _energyLimitNotifier = energyLimitNotifier;
            _levelGoalNotifier = levelGoalNotifier;

            _energyLimitNotifier.LimitExpired += OnLimitExpired;
            _levelGoalNotifier.GoalReached += OnGoalReached;
        }

        private void OnGoalReached()
        {
            GameWon?.Invoke();
        }

        private void OnLimitExpired()
        {
            GameLost?.Invoke();
        }
    }
}