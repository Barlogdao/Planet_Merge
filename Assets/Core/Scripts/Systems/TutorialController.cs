using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System;
using PlanetMerge.Utils.Extensions;

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

        [SerializeField] private Canvas _uiPanelCanvas;
        [SerializeField, SortingLayer] int _tutorialLayer;


        private PlayerInput _playerInput;
        private float _minAlphaValue = 0f;
        private float _maxAlphaValue;

        public event Action IsClicked;

        public void Initialize(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _maxAlphaValue = _background.color.a;
            _background.color = _background.color.WithAlpha(_minAlphaValue);

            _firstTip.Initialize(this, _pointer);
            _secondTip.Initialize(this, _pointer);
            _thirdTip.Initialize(this, _pointer);
            _fourthTip.Initialize(this, _pointer);

            _playerInput.ClickedUp += OnClickedUp;

            _tutorialCanvas.enabled = false;
        }

        private void OnDestroy()
        {
            _playerInput.ClickedUp -= OnClickedUp;
        }

        public async UniTaskVoid ShowTitorial()
        {
            _tutorialCanvas.enabled = true;

            await UnfadeBackground();
            await _firstTip.Run();
            await RunTipInterval();
            await _secondTip.Run();
            await RunTipInterval();

            _playerInput.enabled = false;

            int _uiLayer = _uiPanelCanvas.sortingLayerID;
            _uiPanelCanvas.overrideSorting = true;
            _uiPanelCanvas.sortingLayerID = _tutorialLayer;

            await _thirdTip.Run();
            await _fourthTip.Run();

            _uiPanelCanvas.overrideSorting = false;
            _uiPanelCanvas.sortingLayerID = _uiLayer;

            _playerInput.enabled = true;
            _tutorialCanvas.enabled = false;
        }

        private async UniTask RunTipInterval()
        {
            _playerInput.enabled = false;
            await FadeBackground();
            await UniTask.WaitForSeconds(_intervalDuration);
            await UnfadeBackground();
            _playerInput.enabled = true;
        }

        private async UniTask FadeBackground()
        {
            await _background.DOFade(_minAlphaValue, _fadeDuration);
        }

        private async UniTask UnfadeBackground()
        {
            await _background.DOFade(_maxAlphaValue, _fadeDuration);
        }

        private void OnClickedUp()
        {
            IsClicked?.Invoke();
        }
    }
}