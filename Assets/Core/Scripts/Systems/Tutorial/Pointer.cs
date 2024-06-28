using UnityEngine;

namespace PlanetMerge.Systems.Tutorial
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class Pointer : MonoBehaviour
    {
        private const string Scaling = nameof(Scaling);
        private const string Idle = nameof(Idle);

        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Move(Vector3 atPosition)
        {
            transform.position = atPosition;
        }

        public void SetIdle()
        {
            _animator.Play(Idle);
        }

        public void SetScaling()
        {
            _animator.Play(Scaling);
        }

        public void Activate()
        {
            _spriteRenderer.enabled = true;
        }

        public void Deactivate()
        {
            _spriteRenderer.enabled = false;
        }
    }
}