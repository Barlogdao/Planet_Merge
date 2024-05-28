using Cysharp.Threading.Tasks;
using PlanetMerge.SDK.Yandex;
using PlanetMerge.Systems.SaveLoad;
using PlanetMerge.UI;
using UnityEngine;

public class EndLevelHandler
{
    private readonly GameUi _gameUI;
    private readonly PlayerDataService _playerDataService;
    private readonly ScoreHandler _scoreHandler;
    private readonly IReadOnlyPlayerData _playerData;

    public EndLevelHandler(GameUi gameUI, PlayerDataService playerDataService, ScoreHandler scoreHandler)
    {
        _gameUI = gameUI;
        _playerDataService = playerDataService;
        _scoreHandler = scoreHandler;
        _playerData = _playerDataService.PlayerData;
    }

    public async UniTask Win()
    {
        int levelScore = _scoreHandler.GetScore();

        await _gameUI.ShowLevelScoreAsync(levelScore);
       // �������� ��������� � ��������� ����
        
        //������� ���� ������
        _gameUI.ShowVictoryWindow(_playerData);

        //�������� ������
        UpdatePlayerData(levelScore);

        //��������� �������� ����������
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
    }
}
