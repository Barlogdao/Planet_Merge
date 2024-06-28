using System;
using PlanetMerge.Entities.Planets;
using PlanetMerge.Systems.Events;
using UnityEngine;

namespace PlanetMerge.Systems.Gameplay
{
    public class LevelGoalController : MonoBehaviour, ILevelGoalNotifier
    {
        private int _planetGoalRank;
        private int _planetsToMergeAmount;
        private IPlanetEvents _planetEvents;

        public event Action GoalReached;

        public event Action<int> GoalChanged;

        public int PlanetGoalRank => _planetGoalRank;

        private void OnDestroy()
        {
            _planetEvents.PlanetMerged -= OnPlanetMerged;
        }

        public void Initialize(IPlanetEvents planetEvents)
        {
            _planetEvents = planetEvents;

            _planetEvents.PlanetMerged += OnPlanetMerged;
        }

        public void Prepare(int planetsToMergeAmount, int planetRank)
        {
            _planetGoalRank = planetRank;
            _planetsToMergeAmount = planetsToMergeAmount;

            GoalChanged?.Invoke(_planetsToMergeAmount);
        }

        private void OnPlanetMerged(Planet planet)
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