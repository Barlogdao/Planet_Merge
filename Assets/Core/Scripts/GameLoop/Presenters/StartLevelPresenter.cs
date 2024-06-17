using Cysharp.Threading.Tasks;
using DG.Tweening;
using PlanetMerge.Entities.Walls;
using PlanetMerge.Systems.PlanetLaunching;
using PlanetMerge.UI.View;
using UnityEngine;

namespace PlanetMerge.Gameloop.Presenter
{
    public class StartLevelPresenter : MonoBehaviour
    {
        private const float MinAlpha = 0f;
        private const float MaxAlpha = 1f;

        [SerializeField] private LauncherView _launcherView;
        [SerializeField] private UiPanelView _uiPanelView;
        [SerializeField] private WallsView _wallsView;
        [SerializeField] private VictoryWindow _victoryWindow;
        [SerializeField] private AppearWindow _looseWindow;
        [SerializeField] private SpaceBackground _spaceBackground;
        [SerializeField] private CanvasGroup _buttonsGroup;
        [SerializeField] private float _buttonFadeDuration = 1f;

        public async UniTask StartLevelAsync()
        {
            ResetView();

            await _uiPanelView.AppearAsync();

            await UniTask.WhenAll(
                _launcherView.AppearAsync(),
                _wallsView.AppearAsync(),
                _buttonsGroup.DOFade(MaxAlpha, _buttonFadeDuration).ToUniTask());

            _spaceBackground.Run();
        }

        public void ResumeLevel()
        {
            HideWindows();
        }

        public void HideWindows()
        {
            _victoryWindow.Hide();
            _looseWindow.Hide();
        }

        public async UniTask EndLevelAppear()
        {
            await UniTask.WhenAll(
                _wallsView.DisappearAsync(),
                _uiPanelView.DisappearAsync());

            await _launcherView.DisappearAsync();
        }

        private void ResetView()
        {
            HideWindows();
            _buttonsGroup.alpha = MinAlpha;

            _spaceBackground.Stop();
            _launcherView.ResetView();
            _wallsView.ResetView();
            _uiPanelView.ResetView();
        }
    }
}