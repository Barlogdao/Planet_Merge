using System;
using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.Events;
using PlanetMerge.UI.View;
using UnityEngine;

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

        [SerializeField] private int _shiftedSortingOrder;
        [SerializeField] private float _intervalDuration = 1.5f;

        private InputController _inputController;
        private GameEventMediator _gameEventMediator;
        private int _originSortingOrder;

        public event Action PlanetLaunched;

        private void OnDestroy()
        {
            _gameEventMediator.PlanetLaunched -= OnPlanetLauched;
        }

        public void Initialize(InputController inputController, GameEventMediator gameEventMediator)
        {
            _inputController = inputController;
            _originSortingOrder = _tutorialCanvas.sortingOrder;
            _gameEventMediator = gameEventMediator;

            _firstTip.Initialize(this, _pointer);
            _secondTip.Initialize(this, _pointer);
            _thirdTip.Initialize(this, _pointer);
            _fourthTip.Initialize(this, _pointer);
            _background.Initialize();

            _gameEventMediator.PlanetLaunched += OnPlanetLauched;

            _tutorialCanvas.enabled = false;
        }

        public async UniTaskVoid RunTutorialAsync()
        {
            _tutorialCanvas.enabled = true;
            _tutorialCanvas.sortingOrder = _originSortingOrder;

            await _background.UnfadeAsync();
            await _firstTip.RunAsync();
            await RunIntervalAsync();
            await _secondTip.RunAsync();
            await RunIntervalAsync();

            _inputController.DisableInput();
            _tutorialCanvas.sortingOrder = _shiftedSortingOrder;

            await _thirdTip.RunAsync();
            await _fourthTip.RunAsync();

            _tutorialCanvas.sortingOrder = _originSortingOrder;
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

        private void OnPlanetLauched()
        {
            PlanetLaunched?.Invoke();
        }
    }
}