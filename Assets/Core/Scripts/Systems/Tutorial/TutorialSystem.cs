using Cysharp.Threading.Tasks;
using UnityEngine;
using NaughtyAttributes;
using System;
using PlanetMerge.UI;

namespace PlanetMerge.Systems.Tutorial
{
    public class TutorialSystem : MonoBehaviour
    {
        [SerializeField] private Pointer _pointer;

        [SerializeField] private FirstTip _firstTip;
        [SerializeField] private SecondTip _secondTip;
        [SerializeField] private ButtonTutorialTip _thirdTip;
        [SerializeField] private ButtonTutorialTip _fourthTip;

        [SerializeField] private Canvas _tutorialCanvas;
        [SerializeField] private Canvas _uiPanelCanvas;
        [SerializeField] private FadingBackground _background;

        [SerializeField, SortingLayer] int _tutorialLayer;
        [SerializeField] private float _intervalDuration = 1.5f;

        private PlayerInput _playerInput;
        private InputController _inputController;

        public event Action IsClicked;

        public void Initialize(PlayerInput playerInput, InputController inputController)
        {
            _playerInput = playerInput;
            _inputController = inputController;

            _firstTip.Initialize(this, _pointer);
            _secondTip.Initialize(this, _pointer);
            _thirdTip.Initialize(this, _pointer);
            _fourthTip.Initialize(this, _pointer);
            _background.Initialize();

            _playerInput.ClickedUp += OnClickedUp;

            _tutorialCanvas.enabled = false;
        }

        private void OnDestroy()
        {
            _playerInput.ClickedUp -= OnClickedUp;
        }

        public async UniTaskVoid RunTutorialAsync()
        {
            _tutorialCanvas.enabled = true;

            await _background.UnfadeAsync();
            await _firstTip.Run();
            await RunIntervalAsync();
            await _secondTip.Run();
            await RunIntervalAsync();

            _inputController.DisableInput();

            int _uiLayer = _uiPanelCanvas.sortingLayerID;
            _uiPanelCanvas.overrideSorting = true;
            _uiPanelCanvas.sortingLayerID = _tutorialLayer;

            await _thirdTip.Run();
            await _fourthTip.Run();

            _uiPanelCanvas.overrideSorting = false;
            _uiPanelCanvas.sortingLayerID = _uiLayer;

            _inputController.EnableInput();

            _tutorialCanvas.enabled = false;
        }

        private async UniTask RunIntervalAsync()
        {
            _inputController.DisableInput();
            await _background.FadeAsync();

            await UniTask.WaitForSeconds(_intervalDuration);

            await _background.UnfadeAsync();
            _inputController.EnableInput();
        }

        private void OnClickedUp()
        {
            IsClicked?.Invoke();
        }
    }
}