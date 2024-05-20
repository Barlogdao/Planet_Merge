using PlanetMerge.Planets;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class Trajectory : MonoBehaviour
    {
        [SerializeField] private float _distance = 10f;
        [SerializeField] private LayerMask _collideMask;

        [SerializeField] private TrajectoryLine _mainLine;
        [SerializeField] private TrajectoryLine _collisionLine;
        [SerializeField] private TrajectoryCollisionView _collisionView;

        private Vector2 _startPoint;
        private PlanetLauncher _planetLauncher;
        private float _planetRadius;

        public bool IsActive { get; private set; } = false;

        public void Initialize(PlanetLauncher planetLauncher, float planetRadius)
        {
            _planetLauncher = planetLauncher;
            _startPoint = _planetLauncher.LaunchPosition;
            _planetRadius = planetRadius;

            _mainLine.Initialize(_startPoint);
            _collisionLine.Initialize(_startPoint);
            _collisionView.Initialize(_planetRadius);

            Deactivate();
        }

        private void Update()
        {
            if (IsActive)
                Calculate();
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;

            _mainLine.Hide();
            _collisionLine.Hide();
            _collisionView.Hide();
        }

        private void Calculate()
        {
            //var targetPosition = MousePosition;
            //targetPosition.y = Mathf.Clamp(targetPosition.y, _startPoint.y + 1f, 10f);

            Vector2 direction = _planetLauncher.GetLaunchDirection();


            RaycastHit2D hit = Physics2D.CircleCast(_startPoint, _planetRadius, direction, _distance, _collideMask);

            if (hit.collider != null)
            {
                _collisionLine.SetEndPosition(hit.centroid);
                _collisionView.SetPosition(hit.centroid);
                _mainLine.SetEndPosition(_startPoint + (hit.centroid - _startPoint).normalized * _distance);
            }
            else
            {
                _collisionLine.Hide();
                _collisionView.Hide();
                _mainLine.SetEndPosition(_startPoint + direction.normalized * _distance);
            }
        }
    }
}