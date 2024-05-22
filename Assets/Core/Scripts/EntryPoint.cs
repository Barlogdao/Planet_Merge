using PlanetMerge.Configs;
using PlanetMerge.Data;
using PlanetMerge.Planets;
using PlanetMerge.SDK.Yandex;
using PlanetMerge.Systems;
using PlanetMerge.Systems.Events;
using PlanetMerge.Systems.SaveLoad;
using PlanetMerge.UI;

using UnityEngine;

public class EntryPoint : MonoBehaviour
{
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
    [SerializeField] private UiPanel _uiPanel;

    private PlanetSpawner _planetSpawner;
    private PlanetPool _planetPool;
    private PlanetLimit _planetLimit;
    private LevelConditions _levelConditions;
    private LevelPreparer _levelPreparer;
    private StartLevelHandler _startLevelHandler;
    private EndLevelHandler _endLevelHandler;
    private PlayerDataService _playerDataService;
    private ScoreHandler _scoreHandler;

    private void Awake()
    {
        _playerDataService = new PlayerDataService();

        _planetPool = new PlanetPool(_planetPrefab, _planetHolder);
        _planetSpawner = new PlanetSpawner(_planetPool);
        _planetLimit = new PlanetLimit();

        _gameEventMediator.Initialize(_planetPool, _gameOverHandler, _gameLoop, _gameUI);


        _inputController.Initialize(_playerInput, _gameEventMediator);
        _levelPlanets.Initialize(_gameEventMediator);
        _scoreHandler = new ScoreHandler(_levelPlanets);

        _goalHandler.Initialize(_gameEventMediator);
        _limitHandler.Initialize(_gameEventMediator, _planetLimit);
        _gameOverHandler.Initialize(_limitHandler, _goalHandler);
        _uiPanel.Initialize(_limitHandler, _goalHandler);
        _gameUI.Initialize(_uiPanel);
        _rewardHandler.Initialize(_limitHandler);
        _levelConditions = new LevelConditions(_goalHandler, _limitHandler);

        _planetLauncher.Initialize(_playerInput, _planetSpawner, _planetLimit, _trajectory);
        float planetRadius = _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;
        _trajectory.Initialize(_planetLauncher, planetRadius);


        _levelGenerator.Initialize(_planetSpawner, _levelConditions, _planetLauncher);
        _levelPreparer = new LevelPreparer(_levelGenerator, _levelPlanets, _gameUI);

        _startLevelHandler = new StartLevelHandler(_gameUI, _levelPreparer);
        _endLevelHandler = new EndLevelHandler(_gameUI,_rewardHandler,_playerDataService,_scoreHandler);
        _gameLoop.Initialize(_gameEventMediator, _playerDataService, _startLevelHandler,_endLevelHandler);
    }

    private void Start()
    {
        _gameLoop.Run();
    }

    [ContextMenu ("Reset")]
    public void Resets()
    {
        _playerDataService.Reset();
    }
}
