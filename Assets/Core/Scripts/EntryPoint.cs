using PlanetMerge.Configs;
using PlanetMerge.Data;
using PlanetMerge.Planets;
using PlanetMerge.SDK.Yandex;
using PlanetMerge.Systems;
using PlanetMerge.Systems.Events;
using PlanetMerge.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    [SerializeField] private Planet _planetPrefab;
    [SerializeField] private Transform _planetHolder;

    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlanetLauncher _planetLauncher;
    [SerializeField] private LevelGoalHandler _goalHandler;
    [SerializeField] private PlanetLimitHandler _limitHandler;
    [SerializeField] private GameEventMediator _gameEventMediator;

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private GameOverHandler _gameOverHandler;
    [SerializeField] private GameUi _gameUI;

    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private LevelPlanets _levelPlanets;
    [SerializeField] private InputController _inputController;

    [SerializeField] private RewardHandler _rewardHandler;


    private PlanetSpawner _planetSpawner;
    private PlanetPool _planetPool;
    private PlanetLimit _planetLimit;
    private LevelConditions _levelConditions;
    private LevelPreparer _levelPreparer;
    private StartLevelHandler _startLevelHandler;
    private EndLevelHandler _endLevelHandler;

    private void Awake()
    {
        _planetPool = new PlanetPool(_planetPrefab, _planetHolder);
        _planetSpawner = new PlanetSpawner(_planetPool);
        _planetLimit = new PlanetLimit();
        _levelConditions = new LevelConditions(_goalHandler, _limitHandler);

        _levelPreparer = new LevelPreparer(_levelGenerator, _levelPlanets, _gameUI);


        _gameEventMediator.Initialize(_planetPool, _gameOverHandler, _gameLoop, _gameUI);
        _inputController.Initialize(_playerInput, _gameEventMediator);
        _levelPlanets.Initialize(_gameEventMediator);

        _goalHandler.Initialize(_gameEventMediator);
        _limitHandler.Initialize(_gameEventMediator, _planetLimit);
        _gameOverHandler.Initialize(_limitHandler, _goalHandler);

        _planetLauncher.Initialize(_playerInput, _planetSpawner, _planetLimit, _trajectory);
        float planetRadius = _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;
        _trajectory.Initialize(_planetLauncher, planetRadius);

        _rewardHandler.Initialize(_limitHandler);

        _levelGenerator.Initialize(_planetSpawner, _levelConditions, _planetLauncher);
        _gameUI.Initialize(_limitHandler, _goalHandler);

        _startLevelHandler = new StartLevelHandler();
        _endLevelHandler = new EndLevelHandler(_gameUI,_rewardHandler);
        _gameLoop.Initialize(_gameEventMediator, _playerData, _levelPreparer,_startLevelHandler,_endLevelHandler);
    }

    private void Start()
    {
        _gameLoop.Run();
    }
}
