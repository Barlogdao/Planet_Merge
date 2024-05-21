using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.SaveLoad;
using System;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private GameEventMediator _gameEventMediator;

    private StartLevelHandler _startLevelHandler;
    private EndLevelHandler _endLevelHandler;
    private PlayerDataService _playerDataService;
    private IReadOnlyPlayerData _playerData;

    public event Action LevelPrepared;
    public event Action LevelStarted;
    public event Action LevelResumed;

    public void Initialize(GameEventMediator gameEventMediator, PlayerDataService playerDataService, StartLevelHandler startLevelHandler, EndLevelHandler endLevelHandler)
    {
        _gameEventMediator = gameEventMediator;
        _playerDataService = playerDataService;
        _startLevelHandler = startLevelHandler;
        _endLevelHandler = endLevelHandler;
        _playerData = _playerDataService.PlayerData;

        _gameEventMediator.GameWon += OnGameWon;
        _gameEventMediator.GameLost += OnGameLost;

        _gameEventMediator.NextLevelSelected += OnNextLevelSelected;
        _gameEventMediator.RestartLevelSelected += OnRestartLevelSelected;
        _gameEventMediator.RewardSelected += OnRewardSelected;
    }

    private void OnDestroy()
    {
        _gameEventMediator.GameWon -= OnGameWon;
        _gameEventMediator.GameLost -= OnGameLost;

        _gameEventMediator.NextLevelSelected -= OnNextLevelSelected;
        _gameEventMediator.RestartLevelSelected -= OnRestartLevelSelected;
        _gameEventMediator.RewardSelected -= OnRewardSelected;
    }

    public void Run()
    {
        PrepareLevel();
    }

    private void PrepareLevel()
    {
        _startLevelHandler.PrepareLevel(_playerData);
        LevelPrepared?.Invoke();

        StartLevel().Forget();
    }

    private async UniTaskVoid StartLevel()
    {
        await _startLevelHandler.StartLevel(_playerData.Level);
        LevelStarted?.Invoke();
    }

    private void OnGameWon()
    {
        _endLevelHandler.Win();
    }

    private void OnGameLost()
    {
        _endLevelHandler.Loose();
    }

    private void OnNextLevelSelected()
    {
        PrepareLevel();
    }

    private void OnRestartLevelSelected()
    {
        PrepareLevel();
    }

    private void OnRewardSelected()
    {
        _endLevelHandler.AddReward();
        _startLevelHandler.ResumeLevel();
        LevelResumed?.Invoke();
    }
}
