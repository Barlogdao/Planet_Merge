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
        [SerializeField] private LayerMask _collidableMask;
        [SerializeField] private Transform _collideVisual;

        private SpriteRenderer _collisionSprite;
        private Vector2 _startPoint;
        private  float _planetRadius;

        private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        public void Initialize(Vector2 startPoint, float planetRadius)
        {
            _startPoint = startPoint;
            _planetRadius = planetRadius;
            _collisionSprite = _collideVisual.GetComponent<SpriteRenderer>();
            _linerenderer.positionCount = PositionsCount;
            _linerenderer.SetPosition(LineStart, _startPoint);

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

        private void Calculate()
        {
            RaycastHit2D hit = Physics2D.CircleCast(_startPoint, _planetRadius, MousePosition - _startPoint, 10f,_collidableMask);

            if (hit.collider != null)
            {
                _collideVisual.position = hit.centroid;
                _collisionSprite.enabled = true;
            }
            else
            {
                _collisionSprite.enabled = false;
            }

            _linerenderer.SetPosition(LineEnd, MousePosition);
        }

        public void Hide()
        {
            _linerenderer.enabled = false;
            _collisionSprite.enabled = false;
        }
    }
}