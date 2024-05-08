using PlanetMerge.Configs;
using PlanetMerge.Planets;
using System;
using UnityEngine;


namespace PlanetMerge.Systems
{
    public class GoalTracker :PlanetMergeTracker
    {
        private int _planetGoalRank;
        private int _planetsToMergeAmount;
        private IPlanetStatusNotifier _planetStatusNotifier;

        public event Action GoalReached;

        public void Initialize(IPlanetStatusNotifier planetStatusNotifier)
        {
            _planetStatusNotifier = planetStatusNotifier;

            _planetStatusNotifier.PlanetCreated += OnPlanetCreated;
            _planetStatusNotifier.PlanetReleased += OnPlanetReleased;
        }

        private void OnDestroy()
        {
            _planetStatusNotifier.PlanetCreated -= OnPlanetCreated;
            _planetStatusNotifier.PlanetReleased -= OnPlanetReleased;
        }

        public void Prepare(int planetsToMergeAmount, int planetRank)
        {
            if (planetsToMergeAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetsToMergeAmount));

            if (planetRank <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetRank));

            _planetGoalRank = planetRank;
            _planetsToMergeAmount = planetsToMergeAmount;
        }

        private void OnPlanetCreated(Planet planet)
        {
            planet.Merged += OnPlanetMerged;
        }

        private void OnPlanetReleased(Planet planet)
        {
            planet.Merged -= OnPlanetMerged;
        }

        protected override void OnPlanetMerged(int rank)
        {
            if (_planetGoalRank == rank)
            {
                _planetsToMergeAmount--;
                CheckGoalCondition();
            }
        }

        private void CheckGoalCondition()
        {
            if (_planetsToMergeAmount == 0)
            {
                GoalReached?.Invoke();
                Debug.Log("ÏÀÁÅÄÈËÈ");
            }
        }
    }
}