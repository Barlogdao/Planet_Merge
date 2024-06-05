using Cysharp.Threading.Tasks;
using PlanetMerge.UI;
using UnityEngine;

namespace PlanetMerge.Systems.Visual
{
    public class StartLevelPresenter : MonoBehaviour
    {
        [SerializeField] private LauncherView _launcherView;
        [SerializeField] private UiPanelView _uiPanelView;
        [SerializeField] private WallsView _wallsView;
        [SerializeField] private VictoryWindow _victoryWindow;
        [SerializeField] private AppearWindow _looseWindow;
        [SerializeField] private MovingBackground _movingBackground;

        public async UniTask StartLevelAsync()
        {
            ResetView();

            await _uiPanelView.AppearAsync();

            await UniTask.WhenAll(
                _launcherView.AppearAsync(),
                _wallsView.AppearAsync());

            _movingBackground.enabled = true;
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
                _wallsView.DisapearAsync(),
                _uiPanelView.DisapearAsync());

            await _launcherView.DisapearAsync();
        }

        private void ResetView()
        {
            HideWindows();
            _movingBackground.enabled = false;
            _launcherView.ResetView();
            _wallsView.ResetView();
            _uiPanelView.ResetView();
        }
    }
}