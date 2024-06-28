using UnityEngine;

namespace PlanetMerge.Utils
{
    public class CameraSizeFitter : MonoBehaviour
    {
        private const float MinAspect = 9f / 16f;
        private const float MaxAspect = 9f / 21f;

        [SerializeField] private float _minSize = 5.1f;
        [SerializeField] private float _maxSize = 6f;

        private Camera _camera;
        private float _currentAspect;

        private void Awake()
        {
            _camera = Camera.main;
            _currentAspect = _camera.aspect;

            ChangeSize();
        }

        private void Update()
        {
            if (_currentAspect != _camera.aspect)
            {
                _currentAspect = _camera.aspect;
                ChangeSize();
            }
        }

        private void ChangeSize()
        {
            float t = Mathf.InverseLerp(MinAspect, MaxAspect, _camera.aspect);
            float size = Mathf.Lerp(_minSize, _maxSize, t);

            _camera.orthographicSize = size;
        }
    }
}