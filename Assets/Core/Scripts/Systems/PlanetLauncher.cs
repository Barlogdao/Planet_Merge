
using PlanetMerge.Systems;
using System;
using System.Collections;
using UnityEngine;

namespace PlanetMerge.Planets
{
    public class PlanetLauncher : MonoBehaviour
    {
        [SerializeField] private Trajectory _trajectory;
        [SerializeField] private Transform _launchPoint;

        [SerializeField] private float _force;
        [SerializeField] private float _launchCooldown;

        private PlanetFactory _planetFactory;
        private PlayerInput _playerInput;
        private Planet _loadedPlanet = null;
        private int _planetRank = 1;

        private Coroutine _launchRoutine;
        private WaitForSeconds _cooldown;

        public void Initialize(PlayerInput playerInput, PlanetFactory planetFactory, float planetRadius)
        {
            _playerInput = playerInput;
            _planetFactory = planetFactory;
            _cooldown = new WaitForSeconds(_launchCooldown);

            _trajectory.Initialize(_launchPoint.position, planetRadius);

            _playerInput.ClickedDown += OnClickDown;
            _playerInput.ClickedUp += OnClickUp;
        }

        public void Prepare(int planetRank)
        {
            if (planetRank <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetRank));

            _planetRank = planetRank;

            LoadPlanet();
        }


        private void OnDestroy()
        {
            _playerInput.ClickedDown -= OnClickDown;
            _playerInput.ClickedUp -= OnClickUp;
        }

        private void OnClickDown()
        {
            _trajectory.Show();
        }

        private void OnClickUp()
        {
            if (_launchRoutine == null)
                _launchRoutine = StartCoroutine(LaunchPlanet());

            _trajectory.Hide();
        }


        private IEnumerator LaunchPlanet()
        {
            Vector2 direction = (_playerInput.MousePosition - (Vector2)_launchPoint.position).normalized;

            _loadedPlanet.AddForce(direction * _force);


            yield return _cooldown;

            _launchRoutine = null;
            LoadPlanet();
        }

        private void LoadPlanet()
        {
            _loadedPlanet = _planetFactory.Create(_launchPoint.position, _planetRank);
        }
    }
}