using PlanetMerge.Handlers.Pause;
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
using UnityEditor;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Planet _planetPrefab;
    [SerializeField] private Energy _energyPrefab;
    [SerializeField] private CollisionSplash _collisionSplashPrefab;
    [Header("Holders")]
    [SerializeField] private Transform _planetHolder;
    [SerializeField] private Transform _energyHolder;
    [SerializeField] private Transform _splashHolder;
    [Header("Spawners")]
    [SerializeField] private EnergySpawner _energySpawner;
    [SerializeField] private CollisionSplashSpawner _collisionSplashSpawner;
    [Header("Services")]
    [SerializeField] private AudioService _audioService;
    [Header("Controllers")]
    [SerializeField] private LevelPlanetsController _levelPlanetsController;
    [SerializeField] private MuteController _muteController;
    [SerializeField] private FocusController _focusController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private LevelGoalController _levelGoalController;
    [SerializeField] private EnergyLimitController _energyLimitController;
    [Header("Handlers")]
    [SerializeField] private GameOverHandler _gameOverHandler;
    [SerializeField] private AudioHandler _audioHandler;
    [SerializeField] private RewardHandler _rewardHandler;
    [Header("UI")]
    [SerializeField] private UiPanel _uiPanel;
    [SerializeField] private GameUi _gameUI;
    [Header("Systems")]
    [SerializeField] private PlanetLauncher _planetLauncher;
    [SerializeField] private TutorialSystem _tutorialSystem;
    [SerializeField] private LevelGenerator _levelGenerator;
    [Header("Game Loop")]
    [SerializeField] private StartLevelPresenter _startLevelPresenter;
    [SerializeField] private EndLevelPresenter _endLevelPresenter;
    [SerializeField] private GameLoop _gameLoop;
    [Space]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private YandexLeaderboard _leaderboard;
    [SerializeField] private GameEventMediator _gameEventMediator;

    private PlanetPool _planetPool;
    private EntityPool<Energy> _energyPool;
    private CollisionSplashPool _collisionSplashPool;

    private PlanetSpawner _planetSpawner;

    private PauseService _pauseService;
    private PlayerDataService _playerDataService;

    private InterstitialHandler _interstitialHandler;
    private ScoreHandler _scoreHandler;

    private LevelConditions _levelConditions;
    private LevelPrepareSystem _levelPrepareSystem;
    private PlayerDataSystem _playerDataSystem;

    private void Awake()
    {
        InitializePools();
        InitializeSpawners();

        InitializeServices();
        InitializeControllers();
        InitializeHandlers();
        InitializeUi();
        InitializeSystems();
        InitializeGameLoop();

        _gameEventMediator.Initialize(_planetPool, _gameOverHandler, _gameLoop, _gameUI);
    }

    private void InitializePools()
    {
        _planetPool = new PlanetPool(_planetPrefab, _planetHolder);
        _energyPool = new EntityPool<Energy>(_energyPrefab, _energyHolder);
        _collisionSplashPool = new CollisionSplashPool(_collisionSplashPrefab, _splashHolder);
    }

    private void InitializeSpawners()
    {
        _planetSpawner = new PlanetSpawner(_planetPool);
        _energySpawner.Initialize(_gameEventMediator, _planetLauncher, _energyPool);
        _collisionSplashSpawner.Initialize(_gameEventMediator, _collisionSplashPool);
    }

    private void InitializeServices()
    {
        _pauseService = new PauseService(_audioService);
        _playerDataService = new PlayerDataService();
    }

    private void InitializeControllers()
    {
        _focusController.Initialize(_pauseService);
        _muteController.Initialize(_audioService);
        _inputController.Initialize(_playerInput, _gameEventMediator);
        _levelPlanetsController.Initialize(_gameEventMediator);
        _energyLimitController.Initialize(_gameEventMediator);
        _levelGoalController.Initialize(_gameEventMediator);
    }

    private void InitializeHandlers()
    {
        _scoreHandler = new ScoreHandler(_levelPlanetsController);
        _gameOverHandler.Initialize(_energyLimitController, _levelGoalController);
        _audioHandler.Initialize(_audioService, _gameEventMediator);

        _interstitialHandler = new InterstitialHandler(_pauseService);
        _rewardHandler.Initialize(_energyLimitController, _pauseService);
    }

    private void InitializeUi()
    {
        _uiPanel.Initialize(_energyLimitController, _levelGoalController);
        _gameUI.Initialize(_uiPanel);
    }

    private void InitializeSystems()
    {
        _planetLauncher.Initialize(_playerInput, _planetSpawner, _energyLimitController);
        _levelConditions = new LevelConditions(_levelGoalController, _energyLimitController);
        _levelGenerator.Initialize(_planetSpawner, _levelConditions, _planetLauncher);
        _levelPrepareSystem = new LevelPrepareSystem(_levelGenerator, _levelPlanetsController, _gameUI);

        _tutorialSystem.Initialize(_playerInput, _inputController);
        _playerDataSystem = new PlayerDataSystem(_scoreHandler, _playerDataService, _leaderboard);
    }

    private void InitializeGameLoop()
    {
        IReadOnlyPlayerData playerData = _playerDataService.PlayerData;
        
        PrepareLevelState prepareLevelState = new (playerData,_levelPrepareSystem);
        StartLevelState startLevelState = new (playerData, _tutorialSystem, _startLevelPresenter);
        EndLevelState endLevelState = new (playerData,_endLevelPresenter, _playerDataSystem);
        LevelStates levelStates = new (prepareLevelState, startLevelState, endLevelState);

        _gameLoop.Initialize(_gameEventMediator, levelStates, _rewardHandler, _interstitialHandler);
    }

    private void Start()
    {
        _gameLoop.Run();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _playerDataService.Reset();
        }
    }
}
