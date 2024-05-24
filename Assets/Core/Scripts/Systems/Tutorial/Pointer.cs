using UnityEngine;

namespace PlanetMerge.Systems.Tutorial
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class Pointer : MonoBehaviour
    {
        private const string Scaling = nameof(Scaling);
        private const string Idle = nameof(Idle);

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Move (Vector3 atPosition)
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

        public void Enable()
        {
            _spriteRenderer.enabled = true;
        }

        public void Disable()
        {
            _spriteRenderer.enabled = false;
        }
    }
}