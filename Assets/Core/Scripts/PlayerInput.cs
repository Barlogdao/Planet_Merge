using PlanetMerge.Planet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Transform _launchPoint;
        [SerializeField] private Rigidbody2D _planetPrefab;
        [SerializeField] private float _force;
        [SerializeField] private float _launchCooldown;

        

        [SerializeField] private Trajectory _trajectory;

        private Rigidbody2D _currentPlanet;
        private Coroutine _launchRoutine;

        private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);  

        private void Awake()
        {
            CreatePlanet();

            float planetRadius = _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;

            _trajectory.Initialize(_launchPoint.position, planetRadius);
            
        }

        private void CreatePlanet()
        {
            _currentPlanet = Instantiate(_planetPrefab, _launchPoint.position, Quaternion.identity);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _trajectory.Show();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_launchRoutine == null)
                    _launchRoutine = StartCoroutine(LaunchPlanet());

                _trajectory.Hide();
            }

        }

        private IEnumerator LaunchPlanet()
        {
            Vector2 direction = (MousePosition - (Vector2)_launchPoint.position).normalized;

            _currentPlanet.AddForce(direction * _force, ForceMode2D.Impulse);


            yield return new WaitForSeconds(_launchCooldown);

            _launchRoutine = null;
            CreatePlanet();
        }
    }
}