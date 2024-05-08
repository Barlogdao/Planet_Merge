using UnityEngine;

public class RotationModule : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed;
    [SerializeField] private float _maxRotationSpeed;

    private float _rotationSpeed;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
    }

    private void Update()
    {
        _transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }
}
