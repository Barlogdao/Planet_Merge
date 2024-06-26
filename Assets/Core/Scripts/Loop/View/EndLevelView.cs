using Cysharp.Threading.Tasks;
using PlanetMerge.Entities.Walls;
using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.PlanetLaunching;
using PlanetMerge.UI.View;
using UnityEngine;

namespace PlanetMerge.Loop.View
{
    public class EndLevelView : MonoBehaviour
    {
        [SerializeField] private Trajectory _trajectory;
        [SerializeField] private VictoryWindow _victoryWindow;
        [SerializeField] private AppearWindow _looseWindow;
        [SerializeField] private LevelScoreWindow _levelScoreWindow;
        [SerializeField] private LauncherView _launcherView;
        [SerializeField] private UiPanelView _uiPanelView;
        [SerializeField] private WallsView _wallsView;

        public async UniTask ShowWinAsync(int levelScore, int currentPlanetRank, IReadOnlyPlayerData playerData)
        {
            _trajectory.Deactivate();

            await UniTask.WhenAll(
                LevelDissapearAync(),
                _levelScoreWindow.ShowScoreAsync(levelScore));

            await _victoryWindow.ShowAsync(levelScore, currentPlanetRank, playerData);
        }

        public async UniTaskVoid ShowLooseAsync()
        {
            _trajectory.Deactivate();
            await _looseWindow.AppearAsync();
        }

        private async UniTask LevelDissapearAync()
        {
            await UniTask.WhenAll(
                _wallsView.DisappearAsync(),
                _uiPanelView.DisappearAsync());

            await _launcherView.DisappearAsync();
        }
    }
}