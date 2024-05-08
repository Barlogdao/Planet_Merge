using PlanetMerge.Planets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class PlanetLimitTracker : MonoBehaviour
    {
        private int _limitAmount;

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

        private void OnPlanetMerged(int rank)
        {
            _limitAmount++;
        }

        public void Prepare(int limitAmount)
        {
            if (limitAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(limitAmount));

            _limitAmount = limitAmount;
        }
    }
}