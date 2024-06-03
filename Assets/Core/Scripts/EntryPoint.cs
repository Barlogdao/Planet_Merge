using DG.Tweening;
using PlanetMerge.Handlers.Pause;
using PlanetMerge.Handlers.Split;
using PlanetMerge.Planets;
using PlanetMerge.SDK.Yandex;
using PlanetMerge.Services.Pause;
using PlanetMerge.Services.Pools;
using PlanetMerge.Sevices.Audio;
using PlanetMerge.Systems;
using PlanetMerge.Systems.Audio;
using PlanetMerge.Systems.SaveLoad;
using PlanetMerge.Systems.Tutorial;
using PlanetMerge.Systems.Visual;
using PlanetMerge.UI;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Planet _planetPrefab;
    [SerializeField] private Energy _energyPrefab;
    [SerializeField] private CollisionSplash _collisionSplashPrefab;

    [SerializeField] private Transform _planetHolder;
    [SerializeField] private Transform _energyHolder;
    [SerializeField] private Transform _splashHolder;

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
    [SerializeField] private SplitHandler _splitHandler;

    [SerializeField] private AudioService _audioService;
    [SerializeField] private AudioHandler _audioHandler;
    [SerializeField] private MuteHandler _muteHandler;
    [SerializeField] private FocusHandler _focusHandler;
    [SerializeField] private TutorialSystem _tutorialSystem;
    [SerializeField] private EnergyCollectSystem _energyCollectSystem;
    [SerializeField] private CollisionSplashSystem _collisionSplashSystem;

    [SerializeField] private YandexLeaderboard _leaderboard;

    [SerializeField] private StartLevelViewController _startLevelViewController;

    private PlanetSpawner _planetSpawner;


    private PlanetPool _planetPool;
    private EntityPool<Energy> _energyPool;
    private CollisionSplashPool _collisionSplashPool;

    private PlanetLimit _planetLimit;
    private LevelConditions _levelConditions;
    private LevelPreparer _levelPreparer;
    private StartLevelHandler _startLevelHandler;
    private EndLevelHandler _endLevelHandler;
    private PlayerDataService _playerDataService;
    private ScoreHandler _scoreHandler;
    private PauseService _pauseService;

    private void Awake()
    {
        _playerDataService = new PlayerDataService();
        _inputController.Initialize(_playerInput, _gameEventMediator);

        InitializeServices();

        InitializePlanetSystem();
        InitializePlanetLauncher();
        InitializeLevelPreparer();

        InitializeHandlers();

        InitializeUi();
        InitializeGameLoop();

        _gameEventMediator.Initialize(_planetPool, _gameOverHandler, _gameLoop, _gameUI);
    }

    private void InitializePlanetSystem()
    {
        _planetPool = new PlanetPool(_planetPrefab, _planetHolder);
        _energyPool = new EntityPool<Energy>(_energyPrefab, _energyHolder);
        _collisionSplashPool = new CollisionSplashPool(_collisionSplashPrefab, _splashHolder);

        _planetSpawner = new PlanetSpawner(_planetPool);
        _planetLimit = new PlanetLimit();
        _levelPlanets.Initialize(_gameEventMediator);
    }

    private void InitializeServices()
    {
        _pauseService = new PauseService(_audioService);
    }

    private void InitializePlanetLauncher()
    {
        _planetLauncher.Initialize(_playerInput, _planetSpawner, _planetLimit, _trajectory);
        float planetRadius = _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;
        _trajectory.Initialize(_gameEventMediator, _planetLauncher, planetRadius);
    }

    private void InitializeLevelPreparer()
    {
        _levelConditions = new LevelConditions(_goalHandler, _limitHandler);
        _levelGenerator.Initialize(_planetSpawner, _levelConditions, _planetLauncher);
        _levelPreparer = new LevelPreparer(_levelGenerator, _levelPlanets, _gameUI);
    }

    private void InitializeHandlers()
    {
        _tutorialSystem.Initialize(_playerInput);

        _limitHandler.Initialize(_gameEventMediator, _planetLimit);
        _goalHandler.Initialize(_gameEventMediator);
        _gameOverHandler.Initialize(_limitHandler, _goalHandler);
        _startLevelViewController.Initialize();

        _scoreHandler = new ScoreHandler(_levelPlanets);
        _startLevelHandler = new StartLevelHandler(_gameUI, _levelPreparer, _tutorialSystem, _startLevelViewController);
        _endLevelHandler = new EndLevelHandler(_gameUI, _playerDataService, _scoreHandler, _leaderboard,_startLevelViewController);

        _focusHandler.Initialize(_pauseService);
        _rewardHandler.Initialize(_limitHandler, _pauseService);
        _splitHandler.Initialize(_gameEventMediator, _planetSpawner, _levelPlanets);
        _audioHandler.Initialize(_audioService, _gameEventMediator);
        _muteHandler.Initialize(_audioService);

        _energyCollectSystem.Initialize(_gameEventMediator, _planetLauncher, _energyPool);
        _collisionSplashSystem.Initialize(_gameEventMediator, _collisionSplashPool);


    }

    private void InitializeUi()
    {
        _uiPanel.Initialize(_limitHandler, _goalHandler);
        _gameUI.Initialize(_uiPanel);
    }

    private void InitializeGameLoop()
    {
        _gameLoop.Initialize(_gameEventMediator, _playerDataService, _startLevelHandler, _endLevelHandler, _rewardHandler);
    }

    private void Start()
    {
        _gameLoop.Run();
    }

    [ContextMenu("Reset")]
    public void Resets()
    {
        _playerDataService.Reset();
    }
}
