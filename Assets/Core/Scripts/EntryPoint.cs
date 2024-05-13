using PlanetMerge.Configs;
using PlanetMerge.Data;
using PlanetMerge.Planets;
using PlanetMerge.Systems;
using PlanetMerge.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    [SerializeField] private Planet _planetPrefab;
    [SerializeField] private Transform _planetHolder;

    [SerializeField] private PlanetFactory _planetFactory;
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlanetLauncher _planetLauncher;
    [SerializeField] private LevelGoalHandler _goalHandler;
    [SerializeField] private PlanetLimitHandler _limitHandler;
    [SerializeField] private GameEventBus _gameEventBus;

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private GameOverHandler _gameOverHandler;
    [SerializeField] private GameUI _gameUI;

    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private PlanetsOnLevel _planetsOnLevel;


    private PlanetPool _planetPool;
    private PlanetLimit _planetLimit;

    public float PlanetRadius => _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;

    private void Awake()
    {
        _planetPool = new PlanetPool(_planetPrefab, _planetHolder);
        _planetLimit = new();

        _planetFactory.Initialize(_planetPool);
        _gameEventBus.Initialize(_planetPool);
        _planetsOnLevel.Initialize(_planetPool);

        _goalHandler.Initialize(_gameEventBus);
        _limitHandler.Initialize(_gameEventBus, _planetLimit);
        _gameOverHandler.Initialize(_limitHandler, _goalHandler);
        _gameUI.Initialize(_limitHandler, _goalHandler);

        _planetLauncher.Initialize(_playerInput, _planetFactory, _planetLimit, PlanetRadius);
        _levelGenerator.Initialize(_planetFactory, _playerData, _goalHandler, _planetLauncher);

        _gameLoop.Initialize(_gameOverHandler, _gameUI, _playerData, _levelGenerator, _limitHandler, _planetsOnLevel, _playerInput);
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
