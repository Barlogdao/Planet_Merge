using Cysharp.Threading.Tasks;
using PlanetMerge.Systems;
using PlanetMerge.Systems.Visual;
using PlanetMerge.UI;
using UnityEngine;

public class EndLevelPresenter : MonoBehaviour
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
            EndLevelAppear(),
            _levelScoreWindow.ShowScoreAsync(levelScore));

        await _victoryWindow.ShowAsync(levelScore,currentPlanetRank,playerData);
    }

    public async UniTaskVoid ShowLooseAsync() 
    {
        _trajectory.Deactivate();
        await _looseWindow.AppearAsync();
    }


    public async UniTask EndLevelAppear()
    {
        await UniTask.WhenAll(
            _wallsView.DisapearAsync(),
            _uiPanelView.DisapearAsync());

        await _launcherView.DisapearAsync();
    }
}
