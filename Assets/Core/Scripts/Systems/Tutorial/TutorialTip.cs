using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace PlanetMerge.Systems.Tutorial
{
    [System.Serializable]
    public abstract class TutorialTip
    {
        [SerializeField] private TMP_Text _tipLabel;
        [SerializeField] private Vector2 _pointerPosition;

        private Pointer _pointer;
        private TutorialSystem _tutorialSystem;

        protected TutorialSystem TutorialSystem => _tutorialSystem;

        protected Pointer Pointer => _pointer;

        public void Initialize(TutorialSystem tutorialSystem, Pointer pointer)
        {
            _tutorialSystem = tutorialSystem;
            _pointer = pointer;
            Deactivate();
        }

        public async UniTask RunAsync()
        {
            Activate();

            _pointer.Move(_pointerPosition);
            await OnRunAsync();

            Deactivate();
        }

        protected abstract UniTask OnRunAsync();

        protected virtual void Activate()
        {
            _tipLabel.enabled = true;
            _pointer.Activate();
        }

        protected virtual void Deactivate()
        {
            _tipLabel.enabled = false;
            _pointer.Deactivate();
        }
    }
}