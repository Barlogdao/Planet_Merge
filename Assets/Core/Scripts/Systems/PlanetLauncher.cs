using PlanetMerge.Systems;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace PlanetMerge.Planets
{
    public class PlanetLauncher : MonoBehaviour
    {
        [SerializeField] private Trajectory _trajectory;
        [SerializeField] private Transform _launchPoint;

        [SerializeField] private float _force;
        [SerializeField] private float _launchCooldown;
        [SerializeField] private float _targetPositionOffsetY;

        private PlayerInput _playerInput;
        private PlanetSpawner _planetSpawner;
        private PlanetLimit _planetLimit;

        private Planet _loadedPlanet = null;
        private int _planetRank = 1;

        private Coroutine _launchRoutine;
        private WaitForSeconds _cooldown;

        private bool IsPlanetLoaded => _loadedPlanet != null;
        public Vector2 LaunchPosition => _launchPoint.position;
        private bool CanLoad => IsPlanetLoaded == false && _planetLimit.HasPlanet;
        private bool CanLaunch => IsPlanetLoaded && _launchRoutine == null;

        public void Initialize(PlayerInput playerInput, PlanetSpawner planetSpawner, PlanetLimit planetLimit, float planetRadius)
        {
            _playerInput = playerInput;
            _planetSpawner = planetSpawner;
            _planetLimit = planetLimit;

            _cooldown = new WaitForSeconds(_launchCooldown);

            _trajectory.Initialize(this, planetRadius);

            _playerInput.ClickedDown += OnClickDown;
            _playerInput.ClickedUp += OnClickUp;
            _planetLimit.AmountChanged += OnLimitChanged;
        }

        private void OnDestroy()
        {
            _playerInput.ClickedDown -= OnClickDown;
            _playerInput.ClickedUp -= OnClickUp;
            _planetLimit.AmountChanged -= OnLimitChanged;
        }

        public void Prepare(int planetRank, int limitAmount)
        {
            if (planetRank <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetRank));

            if (limitAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(limitAmount));

            _loadedPlanet = null;

            _planetRank = planetRank;
            _planetLimit.Prepare(limitAmount);

            LoadPlanet();
        }

        public Vector2 GetLaunchDirection()
        {
            Vector2 targetPosition = _playerInput.MousePosition;
            targetPosition.y = Mathf.Clamp(targetPosition.y, LaunchPosition.y + _targetPositionOffsetY, float.MaxValue);

            return targetPosition - LaunchPosition;
        }

        private void OnClickDown()
        {
            if (IsPlanetLoaded)
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
                _planetLimit.Subtract();
                _launchRoutine = StartCoroutine(LaunchPlanet(_loadedPlanet));
                _loadedPlanet = null;
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
                _loadedPlanet = _planetSpawner.Spawn(LaunchPosition, _planetRank);
            }
        }

        private IEnumerator LaunchPlanet(Planet planet)
        {
            //Vector2 direction = (_playerInput.MousePosition - (Vector2)_launchPoint.position).normalized;

            planet.AddForce(GetLaunchDirection().normalized * _force);

            yield return _cooldown;

            _launchRoutine = null;
            LoadPlanet();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;

            Ray ray = new Ray(new Vector3(LaunchPosition.x, LaunchPosition.y + _targetPositionOffsetY), Vector3.right * 10);
            Gizmos.DrawRay(ray);
        }
    }
}