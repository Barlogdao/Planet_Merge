using Cysharp.Threading.Tasks;
using PlanetMerge.SDK.Yandex;
using PlanetMerge.Systems.SaveLoad;
using PlanetMerge.Systems.Visual;
using PlanetMerge.UI;
using UnityEngine;

public class EndLevelHandler
{
    private readonly GameUi _gameUI;
    private readonly PlayerDataService _playerDataService;
    private readonly ScoreHandler _scoreHandler;
    private readonly IReadOnlyPlayerData _playerData;
    private readonly YandexLeaderboard _leaderboard;
    private readonly StartLevelViewController _startLevelViewController;

    public EndLevelHandler(GameUi gameUI, PlayerDataService playerDataService, ScoreHandler scoreHandler, YandexLeaderboard leaderboard, StartLevelViewController startLevelViewController)
    {
        _gameUI = gameUI;
        _playerDataService = playerDataService;
        _scoreHandler = scoreHandler;
        _leaderboard = leaderboard;
        _startLevelViewController = startLevelViewController;
        _playerData = _playerDataService.PlayerData;
    }

    public async UniTask Win()
    {
        int levelScore = _scoreHandler.GetScore();
        await _startLevelViewController.EndLevelAppear();
        await _gameUI.ShowLevelScoreAsync(levelScore);
        // показать дробление о набранные очки

        //открыть окно победы
        _gameUI.ShowVictoryWindow(_playerData);

        //обновить данные
        UpdatePlayerData(levelScore);

        //запустить анимашку прогрессии
        _gameUI.ShowProgress(_playerData);
    }

    public void Loose()
    {
        _gameUI.ShowLooseWindow();
    }

    private void UpdatePlayerData(int levelScore)
    {
        _playerDataService.LevelUp();

        if (_playerData.Level % Constants.PlanetUpgradeStep == 0)
        {
            _playerDataService.UpgradePlanetRank();
        }

        _playerDataService.AddScore(levelScore);
#if UNITY_WEBGL && !UNITY_EDITOR
        _leaderboard.SetPlayerScore(_playerData.Score);
#endif

    }
}
