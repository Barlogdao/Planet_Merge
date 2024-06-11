using UnityEngine;

namespace PlanetMerge.Systems.PlanetLaunching
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TrajectoryCollisionView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void Initialize(float planetRadius)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            Vector3 collisionScale = Vector3.one * (planetRadius * 2);
            transform.localScale = collisionScale;
        }

        public void Show()
        {
            _spriteRenderer.enabled = true;
        }

        public void Hide()
        {
            _spriteRenderer.enabled = false;
        }

        public void SetPosition(Vector2 atPosition)
        {
            Show();

            transform.position = atPosition;
        }
    }
}