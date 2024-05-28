using PlanetMerge.Systems;
using System;
using System.Collections;
using TMPro;
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

        private PlayerInput _playerInput;
        private PlanetSpawner _planetSpawner;
        private PlanetLimit _planetLimit;
        private Trajectory _trajectory;

        private int _planetRank = 1;

        private Coroutine _launchRoutine;
        private WaitForSeconds _cooldown;

        public Vector2 LaunchPosition => _launchPoint.position;
        private bool CanLoad => _planetLimit.HasPlanet; 
        private bool CanLaunch => CanLoad && _launchRoutine == null;

        public void Initialize(PlayerInput playerInput, PlanetSpawner planetSpawner, PlanetLimit planetLimit, Trajectory trajectory)
        {
            _playerInput = playerInput;
            _planetSpawner = planetSpawner;
            _planetLimit = planetLimit;
            _trajectory = trajectory;

            _cooldown = new WaitForSeconds(_launchCooldown);
            _planetView.transform.position = LaunchPosition;

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

        public void Prepare(int planetRank)
        {
            if (planetRank <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetRank));

            _planetRank = planetRank;
            _planetView.Set(_planetRank);
        }

        public Vector2 GetLaunchDirection()
        {
            Vector2 targetPosition = _playerInput.MousePosition;
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
                _planetLimit.Subtract();
                _launchRoutine = StartCoroutine(LaunchPlanet());
                _planetView.Hide();
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
            var planet = _planetSpawner.Spawn(LaunchPosition, _planetRank);
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