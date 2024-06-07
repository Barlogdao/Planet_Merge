using PlanetMerge.Planets;
using PlanetMerge.Systems.Events;
using System;
using System.Collections;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class EnergyLimitController : MonoBehaviour, IEnergyLimit
    {
        private IPlanetEvents _planetEvents;
        private EnergyLimit _energyLimit;
        private Coroutine _limitCheckRoutine;

        public event Action LimitExpired;
        public event Action<int> LimitChanged;

        public bool HasEnergy => _energyLimit.HasEnergy;

        public void Initialize(IPlanetEvents planetEvents)
        {
            _energyLimit = new EnergyLimit();
            _planetEvents = planetEvents;

            _planetEvents.PlanetMerged += OnPlanetMerged;
        }

        private void OnDestroy()
        {
            _planetEvents.PlanetMerged -= OnPlanetMerged;
        }

        public void SetLimit(int amount)
        {
            _energyLimit.Set(amount);

            OnLimitChanged();
        }

        public bool TrySpendEnergy()
        {
            if (HasEnergy)
            {
                _energyLimit.Subtract();

                OnLimitChanged();

                return true;
            }

            return false;
        }

        private void AddEnergy()
        {
            _energyLimit.Add();
            OnLimitChanged();
        }

        private void OnLimitChanged()
        {
            LimitChanged?.Invoke(_energyLimit.Amount);
            LimitCheck();
        }

        private void LimitCheck()
        {
            if (HasEnergy == false)
            {
                _limitCheckRoutine ??= StartCoroutine(LimitChecking());
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

        private IEnumerator LimitChecking()
        {
            yield return new WaitForSeconds(5f);

            if (HasEnergy == false)
                LimitExpired?.Invoke();
        }

        private void OnPlanetMerged(Planet planet)
        {
            AddEnergy();
        }
    }
}