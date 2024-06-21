using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI.View.Background
{
    [RequireComponent(typeof(RawImage))]
    public class MovingBackground : MonoBehaviour
    {
        [SerializeField, Range(0f, 0.1f)] private float _verticalSpeed;
        [SerializeField, Range(0f, 0.1f)] private float _horizontalSpeed;

        private RawImage _background;
        private Vector2 _rectSize;

        private void Awake()
        {
            _background = GetComponent<RawImage>();
            _rectSize = _background.uvRect.size;
        }

        private void Update()
        {
            float positonX = (_background.uvRect.position.x + _horizontalSpeed * Time.deltaTime) % 1f;
            float positionY = (_background.uvRect.position.y + _verticalSpeed * Time.deltaTime) % 1f;
            Vector2 rectPosition = new Vector2(positonX, positionY);


            _background.uvRect = new Rect(rectPosition, _rectSize);
        }
    }
}