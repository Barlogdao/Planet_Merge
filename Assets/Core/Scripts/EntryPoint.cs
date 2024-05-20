using PlanetMerge.Configs;
using PlanetMerge.Data;
using PlanetMerge.Planets;
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

    [SerializeField] private PlanetSpawner _planetSpawner;
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlanetLauncher _planetLauncher;
    [SerializeField] private LevelGoalHandler _goalHandler;
    [SerializeField] private PlanetLimitHandler _limitHandler;
    [SerializeField] private GameEventMediator _gameEventMediator;

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private GameOverHandler _gameOverHandler;
    [SerializeField] private GameUI _gameUI;

    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private LevelPlanets _planetsOnLevel;
    [SerializeField] private InputController _inputController;


    private PlanetPool _planetPool;
    private PlanetLimit _planetLimit;

    public float PlanetRadius => _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;

    private void Awake()
    {
        _planetPool = new PlanetPool(_planetPrefab, _planetHolder);
        _planetLimit = new();

        _planetSpawner.Initialize(_planetPool);
        _gameEventMediator.Initialize(_planetPool, _gameOverHandler,_levelGenerator, _gameUI);

        _planetsOnLevel.Initialize(_gameEventMediator);
        _goalHandler.Initialize(_gameEventMediator);
        _limitHandler.Initialize(_gameEventMediator, _planetLimit);
        _gameOverHandler.Initialize(_limitHandler, _goalHandler);
        _gameUI.Initialize(_gameEventMediator,_limitHandler, _goalHandler);

        _planetLauncher.Initialize(_playerInput, _planetSpawner, _planetLimit, PlanetRadius);
        _levelGenerator.Initialize(_planetSpawner, _playerData, _goalHandler, _planetLauncher, _gameUI);

        _gameLoop.Initialize(_gameEventMediator, _gameUI, _playerData, _levelGenerator, _limitHandler, _planetsOnLevel);

        _inputController.Initialize(_playerInput, _gameEventMediator);
    }

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        _gameLoop.PrepareLevel();

    }
}
