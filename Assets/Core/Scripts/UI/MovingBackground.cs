using UnityEngine;
using UnityEngine.UI;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] private RawImage _background;
    [SerializeField, Range(0f, 0.1f)] private float _verticalSpeed;
    [SerializeField, Range(0f, 0.1f)] private float _horizontalSpeed;

    private void Update()
    {
        Vector2 positionDelta = new Vector2(_horizontalSpeed, _verticalSpeed) * Time.deltaTime;
        _background.uvRect = new Rect(_background.uvRect.position + positionDelta, _background.uvRect.size);
    }
}