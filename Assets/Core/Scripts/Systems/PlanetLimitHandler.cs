using PlanetMerge.Planets;
using PlanetMerge.Systems.Events;
using System;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class PlanetLimitHandler : MonoBehaviour
    {
        private int _limitAmount;
        private IPlanetEvents _planetEvents;

        public event Action GoalReached;

        public void Initialize(IPlanetEvents planetEvents)
        {
            _planetEvents = planetEvents;

            _planetEvents.PlanetMerged += OnPlanetMerged;
        }
        private void OnDestroy()
        {
            _planetEvents.PlanetMerged -= OnPlanetMerged;

        }

        private void OnPlanetMerged(Planet planet)
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