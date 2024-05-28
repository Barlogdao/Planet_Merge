using Cysharp.Threading.Tasks;
using System;
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

        protected Pointer Pointer => _pointer;
        protected TutorialSystem TutorialController;

        public void Initialize(TutorialSystem tutorialController, Pointer pointer)
        {
            TutorialController = tutorialController;
            _pointer = pointer;
            Deactivate();
        }

        public async UniTask Run()
        {
            Activate(); 

            _pointer.Move(_pointerPosition);
            await OnRun();

            Deactivate();
        }

        protected abstract UniTask OnRun();

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