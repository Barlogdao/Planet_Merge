using PlanetMerge.SDK.Yandex;
using PlanetMerge.Systems.SaveLoad;
using PlanetMerge.UI;
using UnityEngine;

public class EndLevelHandler
{
    private GameUi _gameUI;
    private RewardHandler _rewardHandler;
    private PlayerDataService _playerDataService;
    private IReadOnlyPlayerData _playerData;

    public EndLevelHandler(GameUi gameUI, RewardHandler rewardHandler, PlayerDataService playerDataService)
    {
        _gameUI = gameUI;
        _rewardHandler = rewardHandler;
        _playerDataService = playerDataService;
        _playerData = _playerDataService.PlayerData;
    }

    public void Win()
    {
       // показать дробление о набранные очки
        
        //открыть окно победы
        _gameUI.ShowVictoryWindow(_playerData);

        //обновить данные
        UpdatePlayerData();

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

    private void UpdatePlayerData()
    {
        _playerDataService.LevelUp();

        if (_playerData.Level % Constants.PlanetUpgradeStep == 0)
        {
            _playerDataService.UpgradePlanetRank();
        }

        //насчитать скора?
    }
}
