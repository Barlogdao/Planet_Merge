using Cysharp.Threading.Tasks;
using PlanetMerge.UI;
using UnityEngine;

namespace PlanetMerge.Systems.Visual
{
    public class StartLevelViewController : MonoBehaviour
    {
        [SerializeField] private LauncherView _launcherView;
        [SerializeField] private FadingBackground _whiteScreen;
        [SerializeField] private UiPanel _uiPanel;
        [SerializeField] private WallsViewController _wallsViewController;

        public void Initialize()
        {
            _whiteScreen.Initialize();
        }

        public async UniTask StartLevelAppear()
        {
            _launcherView.ResetView();
            _wallsViewController.ResetView();

            await UniTask.WhenAll(_uiPanel.BeginAnimateAsync(), _whiteScreen.UnfadeAsync());
            await UniTask.WhenAll(
                _uiPanel.EndAnimateAsync(),
                _whiteScreen.FadeAsync(),
                _launcherView.AppearAsync(),
                _wallsViewController.AppearAsync());
        }

        public async UniTask EndLevelAppear()
        {
            await UniTask.WhenAll(
                _wallsViewController.DisapearAsync(),
                _launcherView.DisapearAsync()
                );
        }
    }
}