using PlanetMerge.Planets;
using PlanetMerge.Systems.Events;
using System;
using System.Collections;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class PlanetLimitHandler : MonoBehaviour
    {
        private IPlanetEvents _planetEvents;

        public event Action LimitExpired;
        public event Action<int> LimitChanged;

        private PlanetLimit _planetLimit;
        private Coroutine _limitCheckRoutine;

        public void Initialize(IPlanetEvents planetEvents, PlanetLimit planetLimit)
        {
            _planetEvents = planetEvents;
            _planetLimit = planetLimit;

            _planetEvents.PlanetMerged += OnPlanetMerged;
            _planetLimit.AmountChanged += OnLimitChanged;
        }

        private void OnDestroy()
        {
            _planetEvents.PlanetMerged -= OnPlanetMerged;
            _planetLimit.AmountChanged -= OnLimitChanged;
        }

        private void OnLimitChanged(int amount)
        {
            LimitChanged?.Invoke(amount);

            if (_planetLimit.HasPlanet == false)
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
            _planetLimit.Prepare(amount);
        }

        private IEnumerator LimitCheck()
        {
            yield return new WaitForSeconds(5f);

            if (_planetLimit.HasPlanet == false)
                LimitExpired?.Invoke();
        }

        private void OnPlanetMerged(Planet planet)
        {
            _planetLimit.Add();
        }
    }
}