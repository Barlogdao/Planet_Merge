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

    [SerializeField] private LevelLayout _levelSetup;
    [SerializeField] private LevelGoal _levelGoal;

    [SerializeField] private LevelGenerator _levelGenerator;

    
    private PlanetPool _planetPool;

    public float PlanetRadius => _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;

    private void Awake()
    {
        _planetPool = new PlanetPool(_planetPrefab, _planetHolder);

        _planetFactory.Initialize(_planetPool);
        _gameEventBus.Initialize(_planetPool);

        _goalHandler.Initialize(_gameEventBus);
        _limitHandler.Initialize(_gameEventBus);

        _planetLauncher.Initialize(_playerInput, _planetFactory, PlanetRadius);
        _levelGenerator.Initialize(_planetFactory, _playerData, _goalHandler, _limitHandler,_planetLauncher);


        SetUp();
    }

    private void SetUp()
    {
        _levelGenerator.Generate();

    }


}
