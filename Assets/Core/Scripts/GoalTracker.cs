using PlanetMerge.Configs;
using PlanetMerge.Planets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class GoalTracker : MonoBehaviour
    {
        private int _planetLevel;
        private int _planetMergeAmount;
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
        private void OnPlanetCreated(Planet planet)
        {
            planet.Merged += OnPlanetMerged;
        }

        private void OnPlanetReleased(Planet planet)
        {
            planet.Merged -= OnPlanetMerged;
        }


        private void OnPlanetMerged(int level)
        {
            if (_planetLevel == level)
            {
                _planetMergeAmount--;
            }

            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (_planetMergeAmount == 0)
            {
                GoalReached?.Invoke();
                Debug.Log("œ¿¡≈ƒ»À»");
            }
        }

        public void SetGoal(int planetMergeAmount, int planetLevel)
        {
            _planetLevel = planetLevel;
            _planetMergeAmount = planetMergeAmount;
        }
    }
}