using PlanetMerge.Configs;
using PlanetMerge.Data;
using PlanetMerge.Planets;
using PlanetMerge.Systems;
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
    [SerializeField] private GoalTracker _goalTracker;


    [SerializeField] private LevelSetup _levelSetup;
    [SerializeField] private LevelGoal _levelGoal;

    
    private PlanetPool _planetPool;

    public float PlanetRadius => _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;

    private void Awake()
    {
        _planetPool = new PlanetPool(_planetPrefab, _planetHolder);

        _planetFactory.Initialize(_planetPool);


        _planetLauncher.Initialize(_playerInput, _planetFactory, PlanetRadius);
        _goalTracker.Initialize(_planetPool);

        SetUp();
    }

    private void SetUp()
    {
        _goalTracker.Prepare(_levelGoal.PlanetsMergedAmount, _levelGoal.PlanetLevelModificator + _playerData.PlanetRank);

        foreach(PlanetSetup planetSetup in _levelSetup.PlanetSetups)
        {
            _planetFactory.Create(planetSetup.Position, _playerData.PlanetRank + planetSetup.RankModificator);
        }

        
        _planetLauncher.Prepare(_playerData.PlanetRank);

    }


}
