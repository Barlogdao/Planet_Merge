using PlanetMerge.Configs;
using PlanetMerge.Planets;
using System;
using UnityEngine;
using PlanetMerge.Systems.Events;

namespace PlanetMerge.Systems
{
    public class LevelGoalHandler : MonoBehaviour
    {
        private int _planetGoalRank;
        private int _planetsToMergeAmount;

        private IPlanetEvents _planetEvents;

        public event Action GoalReached;
        public event Action<int> GoalChanged;

        public void Initialize(IPlanetEvents planetEvents)
        {
            _planetEvents = planetEvents;

            _planetEvents.PlanetMerged += OnPlanetMerged;
        }

        private void OnDestroy()
        {
            _planetEvents.PlanetMerged -= OnPlanetMerged;
        }

        public void Prepare(int planetsToMergeAmount, int planetRank)
        {
            if (planetsToMergeAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetsToMergeAmount));

            if (planetRank <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetRank));

            _planetGoalRank = planetRank;
            _planetsToMergeAmount = planetsToMergeAmount;

            GoalChanged?.Invoke(_planetsToMergeAmount);
        }


        protected void OnPlanetMerged(Planet planet)
        {
            if (_planetGoalRank == planet.Rank)
            {
                _planetsToMergeAmount--;

                GoalChanged?.Invoke(_planetsToMergeAmount);
                CheckGoalCondition();
            }
        }

        private void CheckGoalCondition()
        {
            if (_planetsToMergeAmount == 0)
            {
                GoalReached?.Invoke();
            }
        }
    }
}