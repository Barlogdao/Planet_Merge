using UnityEngine;

namespace PlanetMerge.Systems.PlanetLaunching
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryLine : MonoBehaviour
    {
        private const int PositionsCount = 2;
        private const int LineStart = 0;
        private const int LineEnd = 1;

        private LineRenderer _lineRenderer;

        private Vector2 _startPoint;

        public void Initialize(Vector2 startPoint)
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _startPoint = startPoint;

            _lineRenderer.positionCount = PositionsCount;
            _lineRenderer.SetPosition(LineStart, _startPoint);
        }

        public void SetEndPosition(Vector2 atPosition)
        {
            Show();

            _lineRenderer.SetPosition(LineEnd, atPosition);
        }

        public void Show()
        {
            _lineRenderer.enabled = true;
        }

        public void Hide()
        {
            _lineRenderer.enabled = false;
        }
    }
}