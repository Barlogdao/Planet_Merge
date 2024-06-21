using System;
using System.Collections;
using PlanetMerge.Entities.Planets;
using PlanetMerge.Spawners;
using PlanetMerge.Systems.Events;
using PlanetMerge.Systems.PlanetLaunching;
using PlanetMerge.Utils;
using UnityEngine;

namespace PlanetMerge.Systems.Gameplay.PlanetLaunching
{
    public class PlanetLauncher : MonoBehaviour, ILaunchPoint, ILauncherNotifier
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

        private int _planetRank = Constants.MinimalPlanetRank;
        private Coroutine _launchRoutine;
        private WaitForSeconds _cooldown;
        private bool _isLoaded = false;

        public event Action PlanetLaunched;

        public Vector2 LaunchPosition => _launchPoint.position;

        private bool CanLoad => _energyLimit.HasEnergy && _isLoaded == false;

        private bool CanLaunch => _isLoaded && _launchRoutine == null;

        private void OnDestroy()
        {
            _playerInput.ClickedDown -= OnClickDown;
            _playerInput.ClickedUp -= OnClickUp;
            _energyLimit.LimitChanged -= OnLimitChanged;
        }

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

        public void Prepare(int planetRank)
        {
            _planetRank = planetRank;
            _planetView.Set(_planetRank);
            _isLoaded = false;
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
                _launchRoutine = StartCoroutine(LaunchPlanet());
            }
        }

        private void OnLimitChanged(int amount)
        {
            TryLoadPlanet();
        }

        private bool TryLoadPlanet()
        {
            if (CanLoad)
            {
                LoadPlanet();
                return true;
            }

            return false;
        }

        private void LoadPlanet()
        {
            _isLoaded = true;
            _planetView.Show();
        }

        private void UnloadPlanet()
        {
            _isLoaded = false;
            _planetView.Hide();
        }

        private IEnumerator LaunchPlanet()
        {
            _energyLimit.TrySpendEnergy();

            UnloadPlanet();
            PlanetLaunched?.Invoke();

            Planet planet = _planetSpawner.Spawn(LaunchPosition, _planetRank);
            planet.AddForce(GetLaunchDirection().normalized * _force);

            yield return _cooldown;

            _launchRoutine = null;

            TryLoadPlanet();
        }
    }
}