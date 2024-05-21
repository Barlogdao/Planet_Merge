using PlanetMerge.Data;
using System;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private GameEventMediator _gameEventMediator;

    private StartLevelHandler _startLevelHandler;
    private EndLevelHandler _endLevelHandler;
    private PlayerData _playerData;

    private LevelPreparer _levelPreparer;

    public event Action LevelPrepared;
    public event Action LevelStarted;
    public event Action LevelResumed;

    public void Initialize(GameEventMediator gameEventMediator, PlayerData playerData, LevelPreparer levelPreparer, StartLevelHandler startLevelHandler, EndLevelHandler endLevelHandler)
    {
        _gameEventMediator = gameEventMediator;
        _playerData = playerData;
        _levelPreparer = levelPreparer;
        _startLevelHandler = startLevelHandler;
        _endLevelHandler = endLevelHandler;


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
        _levelPreparer.Prepare(_playerData);
        LevelPrepared?.Invoke();

        StartLevel();
    }

    private void StartLevel()
    {
        _startLevelHandler.StartLevel(_playerData.Level);
        LevelStarted?.Invoke();
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

    private void OnGameWon()
    {
        _playerData.Level++;
        _endLevelHandler.Win();
    }

    private void OnGameLost()
    {
        _endLevelHandler.Loose();
    }
}
