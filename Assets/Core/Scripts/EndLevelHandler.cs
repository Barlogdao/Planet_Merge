using Cysharp.Threading.Tasks;
using PlanetMerge.SDK.Yandex;
using PlanetMerge.Systems.SaveLoad;
using PlanetMerge.UI;
using UnityEngine;

public class EndLevelHandler
{
    private readonly GameUi _gameUI;
    private readonly LevelPlanets _levelPlanets;
    private readonly RewardHandler _rewardHandler;
    private readonly PlayerDataService _playerDataService;
    private readonly ScoreHandler _scoreHandler;
    private readonly IReadOnlyPlayerData _playerData;

    public EndLevelHandler(GameUi gameUI, RewardHandler rewardHandler, PlayerDataService playerDataService, ScoreHandler scoreHandler, LevelPlanets levelPlanets)
    {
        _gameUI = gameUI;
        _levelPlanets = levelPlanets;
        _rewardHandler = rewardHandler;
        _playerDataService = playerDataService;
        _scoreHandler = scoreHandler;
        _playerData = _playerDataService.PlayerData;
    }

    public async UniTask Win()
    {
        int levelScore = _scoreHandler.GetScore();

        await _gameUI.ShowLevelScore(levelScore);
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

    public void AddReward()
    {
        _rewardHandler.AddReward();
    }

    private void UpdatePlayerData(int levelScore)
    {
        _playerDataService.LevelUp();

        if (_playerData.Level % Constants.PlanetUpgradeStep == 0)
        {
            _playerDataService.UpgradePlanetRank();
        }

        _playerDataService.AddScore(levelScore);
    }
}
