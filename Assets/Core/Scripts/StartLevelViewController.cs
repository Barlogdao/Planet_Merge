using Cysharp.Threading.Tasks;
using PlanetMerge.UI;
using UnityEngine;

namespace PlanetMerge.Systems.Visual
{
    public class StartLevelViewController : MonoBehaviour
    {
        [SerializeField] private LauncherView _launcherView;
        [SerializeField] private UiPanelView _uiPanelView;
        [SerializeField] private WallsViewController _wallsViewController;

        public async UniTask StartLevelAppear()
        {
            ResetView();

            await _uiPanelView.AppearAsync();

            await UniTask.WhenAll(
                _launcherView.AppearAsync(),
                _wallsViewController.AppearAsync());
        }

        public async UniTask EndLevelAppear()
        {
            await UniTask.WhenAll(
                _wallsViewController.DisapearAsync(),
                _uiPanelView.DisapearAsync());

            await _launcherView.DisapearAsync();
        }

        private void ResetView()
        {
            _launcherView.ResetView();
            _wallsViewController.ResetView();
            _uiPanelView.ResetView();
        }
    }
}