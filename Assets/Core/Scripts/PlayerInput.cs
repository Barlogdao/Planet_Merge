using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private Rigidbody2D _planetPrefab;
    [SerializeField] private float _force;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePosition - _launchPoint.position).normalized;

            var planet = Instantiate(_planetPrefab, _launchPoint.position, Quaternion.identity);
            planet.AddForce(direction * _force, ForceMode2D.Impulse);
        }
    }
}
