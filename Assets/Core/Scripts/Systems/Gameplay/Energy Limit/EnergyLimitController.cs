using System;
using System.Collections;
using PlanetMerge.Entities.Planets;
using PlanetMerge.Systems.Events;
using UnityEngine;

namespace PlanetMerge.Systems.Gameplay
{
    public class EnergyLimitController : MonoBehaviour, IEnergyLimit
    {
        [SerializeField, Min(1f)] private float _limitExpirationDelay = 5f;

        private IPlanetEvents _planetEvents;
        private EnergyLimit _energyLimit;
        private Coroutine _limitCheckRoutine;
        private WaitForSeconds _limitCheckDelay;

        public event Action LimitExpired;
        public event Action<int> LimitChanged;

        public bool HasEnergy => _energyLimit.HasEnergy;

        private void OnDestroy()
        {
            _planetEvents.PlanetMerged -= OnPlanetMerged;
        }

        public void Initialize(IPlanetEvents planetEvents)
        {
            _energyLimit = new EnergyLimit();
            _planetEvents = planetEvents ?? throw new ArgumentNullException(nameof(planetEvents));
            _limitCheckDelay = new WaitForSeconds(_limitExpirationDelay);

            _planetEvents.PlanetMerged += OnPlanetMerged;
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
            yield return _limitCheckDelay;

            if (HasEnergy == false)
                LimitExpired?.Invoke();
        }

        private void OnLimitChanged()
        {
            LimitChanged?.Invoke(_energyLimit.Amount);
            LimitCheck();
        }

        private void OnPlanetMerged(Planet planet)
        {
            AddEnergy();
        }
    }
}