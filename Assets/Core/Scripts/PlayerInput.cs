using PlanetMerge.Planet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private Rigidbody2D _planetPrefab;
    [SerializeField] private float _force;
    [SerializeField] private float _launchCooldownl;

    private Rigidbody2D _currentPlanet;
    private Coroutine _launchRoutine;

    private void Awake()
    {
        CreatePlanet();
    }

    private void CreatePlanet()
    {
        _currentPlanet = Instantiate(_planetPrefab, _launchPoint.position, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_launchRoutine != null)
                _launchRoutine = StartCoroutine(LaunchPlanet());
        }
    }

    private IEnumerator LaunchPlanet()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - _launchPoint.position).normalized;

        _currentPlanet.AddForce(direction * _force, ForceMode2D.Impulse);

         yield return _launchRoutine;

        CreatePlanet();
        _launchRoutine = null;
    }
}
