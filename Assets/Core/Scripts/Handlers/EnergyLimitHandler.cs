using PlanetMerge.Planets;
using PlanetMerge.Systems.Events;
using System;
using System.Collections;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class EnergyLimitHandler : MonoBehaviour
    {
        private IPlanetEvents _planetEvents;
        private EnergyLimit _energyLimit;
        private Coroutine _limitCheckRoutine;

        public event Action LimitExpired;
        public event Action<int> LimitChanged;

        public void Initialize(IPlanetEvents planetEvents, EnergyLimit energyLimit)
        {
            _planetEvents = planetEvents;
            _energyLimit = energyLimit;

            _planetEvents.PlanetMerged += OnPlanetMerged;
            _energyLimit.AmountChanged += OnLimitChanged;
        }

        private void OnDestroy()
        {
            _planetEvents.PlanetMerged -= OnPlanetMerged;
            _energyLimit.AmountChanged -= OnLimitChanged;
        }

        private void OnLimitChanged(int amount)
        {
            LimitChanged?.Invoke(amount);

            if (_energyLimit.HasEnergy == false)
            {
                _limitCheckRoutine ??= StartCoroutine(LimitCheck());
            }
            else
            {
                if (_limitCheckRoutine != null)
                {
                    StopCoroutine(_limitCheckRoutine);
                    _limitCheckRoutine = null;
                }
            }
        }

        public void SetLimit(int amount)
        {
            _energyLimit.Prepare(amount);
        }

        private IEnumerator LimitCheck()
        {
            yield return new WaitForSeconds(5f);

            if (_energyLimit.HasEnergy == false)
                LimitExpired?.Invoke();
        }

        private void OnPlanetMerged(Planet planet)
        {
            _energyLimit.Add();
        }
    }
}