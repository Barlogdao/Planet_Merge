using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class Trajectory : MonoBehaviour
    {
        private const int PositionsCount = 2;
        private const int LineStart = 0;
        private const int LineEnd = 1;

        [SerializeField] private float _lineDistance = 10f;
        [SerializeField] private LayerMask _collideMask;


        [SerializeField] private LineRenderer _mainLine;
        [SerializeField] private LineRenderer _collisionLine;

        [SerializeField] private Transform _collideVisual;
        private SpriteRenderer _collisionSprite;

        private Vector2 _startPoint;
        private float _planetRadius;

        private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        public void Initialize(Vector2 startPoint, float planetRadius)
        {
            _startPoint = startPoint;
            _planetRadius = planetRadius;
            _collisionSprite = _collideVisual.GetComponent<SpriteRenderer>();
            _mainLine.positionCount = PositionsCount;
            _collisionLine.positionCount = PositionsCount;
            _mainLine.SetPosition(LineStart, _startPoint);
            _collisionLine.SetPosition(LineStart, _startPoint);

            Hide();
        }

        private void Update()
        {
            if (_mainLine.enabled)
            {
                Calculate();
            }
        }

        public void Show()
        {
            _mainLine.enabled = true;
        }

        public void Hide()
        {
            _mainLine.enabled = false;
            _collisionSprite.enabled = false;
            _collisionLine.enabled = false;
        }

        private void Calculate()
        {
            Vector2 direction = MousePosition - _startPoint;

            HandleCollision(direction);

            //_mainLine.SetPosition(LineEnd, direction * 5f);
        }

        private void HandleCollision(Vector2 direction)
        {

            RaycastHit2D hit = Physics2D.CircleCast(_startPoint, _planetRadius, direction, _lineDistance, _collideMask);

            if (hit.collider != null)
            {
                _collisionLine.enabled = true;
                _collisionLine.SetPosition(LineEnd, hit.centroid);
                _mainLine.SetPosition(LineEnd, _startPoint + (hit.centroid - _startPoint).normalized * _lineDistance);

                _collisionSprite.enabled = true;
                _collideVisual.position = hit.centroid;
            }
            else
            {
                _collisionLine.enabled = false;
                _collisionSprite.enabled = false;
            }
        }

    }
}