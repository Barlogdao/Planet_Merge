using PlanetMerge.Systems;
using System;
using System.Collections;
using UnityEngine;

namespace PlanetMerge.Planets
{
    public class PlanetLauncher : MonoBehaviour, ILaunchPoint
    {
        [SerializeField] private Transform _launchPoint;
        [SerializeField] private float _force;
        [SerializeField] private float _launchCooldown;
        [SerializeField] private float _targetPositionOffsetY;
        [SerializeField] private PlanetView _planetView;
        [SerializeField] private Trajectory _trajectory;

        private PlayerInput _playerInput;
        private PlanetSpawner _planetSpawner;
        private IEnergyLimit _energyLimit;
        private int _planetRank = 1;
        private Coroutine _launchRoutine;
        private WaitForSeconds _cooldown;

        public event Action PlanetLaunched;

        public Vector2 LaunchPosition => _launchPoint.position;

        private bool CanLoad => _energyLimit.HasEnergy; 
        private bool CanLaunch => CanLoad && _launchRoutine == null;

        public void Initialize(PlayerInput playerInput, PlanetSpawner planetSpawner, IEnergyLimit energyLimit)
        {
            _playerInput = playerInput;
            _planetSpawner = planetSpawner;
            _energyLimit = energyLimit;
            _cooldown = new WaitForSeconds(_launchCooldown);

            _trajectory.Initialize(this);

            _playerInput.ClickedDown += OnClickDown;
            _playerInput.ClickedUp += OnClickUp;
            _energyLimit.LimitChanged += OnLimitChanged;
        }

        private void OnDestroy()
        {
            _playerInput.ClickedDown -= OnClickDown;
            _playerInput.ClickedUp -= OnClickUp;
            _energyLimit.LimitChanged -= OnLimitChanged;
        }

        public void Prepare(int planetRank)
        {
            if (planetRank <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetRank));

            _planetRank = planetRank;
            _planetView.Set(_planetRank);
        }

        public Vector2 GetLaunchDirection()
        {
            Vector2 targetPosition = _playerInput.PointerPosition;
            targetPosition.y = Mathf.Clamp(targetPosition.y, LaunchPosition.y + _targetPositionOffsetY, float.MaxValue);

            return targetPosition - LaunchPosition;
        }

        private void OnClickDown()
        {
            if (CanLaunch)
            {
                _trajectory.Activate();
            }
        }

        private void OnClickUp()
        {
            if (_trajectory.IsActive == false)
                return;

            _trajectory.Deactivate();

            if (CanLaunch)
            {
                _energyLimit.TrySpendEnergy();
                _launchRoutine = StartCoroutine(LaunchPlanet());
            }
        }

        private void OnLimitChanged(int amount)
        {
            LoadPlanet();
        }

        private void LoadPlanet()
        {
            if (CanLoad)
            {
                _planetView.Show();
            }
        }

        private IEnumerator LaunchPlanet()
        {
            _planetView.Hide();
            PlanetLaunched?.Invoke();

            var planet = _planetSpawner.Spawn(LaunchPosition, _planetRank);
            planet.AddForce(GetLaunchDirection().normalized * _force);

            yield return _cooldown;

            _launchRoutine = null;
            LoadPlanet();
        }
    }
}