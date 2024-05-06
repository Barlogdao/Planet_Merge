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

        [SerializeField] private LineRenderer _linerenderer;
        [SerializeField] private LineRenderer _collisionLine;

        [SerializeField] private Transform _collideVisual;
        [SerializeField] ContactFilter2D _contactfilter;

        private SpriteRenderer _collisionSprite;
        private Vector2 _startPoint;
        private float _planetRadius;

        private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        public void Initialize(Vector2 startPoint, float planetRadius)
        {
            _startPoint = startPoint;
            _planetRadius = planetRadius;
            _collisionSprite = _collideVisual.GetComponent<SpriteRenderer>();
            _linerenderer.positionCount = PositionsCount;
            _collisionLine.positionCount = PositionsCount;
            _linerenderer.SetPosition(LineStart, _startPoint);
            _collisionLine.SetPosition(LineStart, _startPoint);

            Hide();
        }

        private void Update()
        {
            if (_linerenderer.enabled)
            {
                Calculate();
            }
        }

        public void Show()
        {
            _linerenderer.enabled = true;
        }

        public void Hide()
        {
            _linerenderer.enabled = false;
            _collisionSprite.enabled = false;
            _collisionLine.enabled = false;
        }

        private void Calculate()
        {
            Vector2 direction = MousePosition - _startPoint;

            HandleCollision(direction);

            //_linerenderer.SetPosition(LineEnd, direction * 5f);
        }

        private void HandleCollision(Vector2 direction)
        {
            RaycastHit2D[] hits = new RaycastHit2D[2];

            if (Physics2D.CircleCast(_startPoint, _planetRadius, direction, _contactfilter, hits, 10f) <= 1)
                return;

            if (hits[1].collider != null)
            {
                _collisionLine.enabled = true;
                _collisionLine.SetPosition(LineEnd, hits[1].centroid);
                _linerenderer.SetPosition(LineEnd, hits[1].centroid - _startPoint);

                _collisionSprite.enabled = true;
                _collideVisual.position = hits[1].centroid;
            }
            else
            {
                _collisionLine.enabled = false;
                _collisionSprite.enabled = false;
            }
        }

    }
}