using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System;

namespace PlanetMerge.Systems.Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private Pointer _pointer;
        [SerializeField] private FirstTip _firstTip;
        [SerializeField] private SecondTip _secondTip;
        [SerializeField] private ButtonTutorialTip _thirdTip;
        [SerializeField] private ButtonTutorialTip _fourthTip;

        [SerializeField] private Canvas _tutorialCanvas;

        [SerializeField] Image _background;
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private float _intervalDuration = 1.5f;
        [SerializeField, SortingLayer] int _uiSortingLayer;


        private PlayerInput _playerInput;
        private Color _backgroundColor;

        public event Action IsClicked;

        public void Initialize(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _backgroundColor = _background.color;

            _firstTip.Initialize(this, _pointer);
            _secondTip.Initialize(this, _pointer);
            _thirdTip.Initialize(this, _pointer);
            _fourthTip.Initialize(this, _pointer);

            _playerInput.ClickedUp += OnClickedUp;
        }

        private void OnDestroy()
        {
            _playerInput.ClickedUp -= OnClickedUp;

        }

        private void Start()
        {
            _tutorialCanvas.enabled = false;
        }

        public async UniTaskVoid ShowTitorial()
        {
            _tutorialCanvas.enabled = true;

            await _firstTip.Run();
            await RunTipInterval();
            await _secondTip.Run();
            await RunTipInterval();

            _playerInput.enabled = false;
            _tutorialCanvas.sortingLayerID = _uiSortingLayer;

            await _thirdTip.Run();
            await _fourthTip.Run();

            _playerInput.enabled = true;
            _tutorialCanvas.enabled = false;
        }

        private async UniTask RunTipInterval()
        {
            FadeBackground();
            await UniTask.WaitForSeconds(_intervalDuration);
            _background.color = _backgroundColor;
        }

        private void FadeBackground()
        {
          _background.DOFade(0f, _fadeDuration);
        }

        private void OnClickedUp()
        {
            IsClicked?.Invoke();
        }
    }
}