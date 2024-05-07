
using PlanetMerge.Systems;
using System;
using System.Collections;
using UnityEngine;

namespace PlanetMerge.Planets
{
    public class PlanetLauncher : MonoBehaviour
    {
        private PlanetFactory _planetFactory;
        [SerializeField] private Trajectory _trajectory;
        private PlayerInput _playerInput;
        [SerializeField] private Transform _launchPoint;
        [SerializeField] private float _force;
        [SerializeField] private float _launchCooldown;

        [SerializeField] private int _launchingLevel = 1;

        private Coroutine _launchRoutine;
        private WaitForSeconds _cooldown;
        private Planet _loadedPlanet = null;


        public void Initialize(PlayerInput playerInput, PlanetFactory planetFactory, float planetRadius)
        {
            _playerInput = playerInput;
            _planetFactory = planetFactory;
            _trajectory.Initialize(_launchPoint.position, planetRadius);

            _cooldown = new WaitForSeconds(_launchCooldown);

            _playerInput.ClickedDown += OnClickDown;
            _playerInput.ClickedUp += OnClickUp;
        }

        public void Prepare()
        {
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
            _loadedPlanet = _planetFactory.Create(_launchPoint.position, _launchingLevel);
        }
    }
}