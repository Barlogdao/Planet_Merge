using PlanetMerge.Entities.Energy;
using PlanetMerge.Entities.Planets;
using PlanetMerge.Entities.Splash;
using PlanetMerge.Gameloop.Presenter;
using PlanetMerge.Gameloop.States;
using PlanetMerge.Pools;
using PlanetMerge.SDK.Yandex;
using PlanetMerge.SDK.Yandex.Advertising;
using PlanetMerge.SDK.Yandex.Leaderboard;
using PlanetMerge.Spawners;
using PlanetMerge.Systems;
using PlanetMerge.Systems.Audio;
using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.Events;
using PlanetMerge.Systems.Gameplay;
using PlanetMerge.Systems.Gameplay.LevelPreparing;
using PlanetMerge.Systems.Gameplay.PlanetLaunching;
using PlanetMerge.Systems.Pause;
using PlanetMerge.Systems.Tutorial;
using PlanetMerge.UI;
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
    [Header("Audio")]
    [SerializeField] private AudioService _audioService;
    [SerializeField] private MuteController _muteController;
    [SerializeField] private AudioHandler _audioHandler;
    [Header("Input")]
    [SerializeField] private InputController _inputController;
    [SerializeField] private PlayerInput _playerInput;
    [Header("Gameplay")]
    [SerializeField] private LevelPlanetsController _levelPlanetsController;
    [SerializeField] private LevelGoalController _levelGoalController;
    [SerializeField] private EnergyLimitController _energyLimitController;
    [SerializeField] private RewardHandler _rewardHandler;
    [SerializeField] private GameOverHandler _gameOverHandler;
    [SerializeField] private PlanetLauncher _planetLauncher;
    [SerializeField] private LevelGenerator _levelGenerator;
    [Header("UI")]
    [SerializeField] private UiPanel _uiPanel;
    [SerializeField] private GameUi _gameUI;
    [Header("SDK")]
    [SerializeField] private FocusController _focusController;
    [SerializeField] private YandexLeaderboard _leaderboard;
    [Header("Tutorial")]
    [SerializeField] private TutorialSystem _tutorialSystem;
    [Header("Game Loop")]
    [SerializeField] private StartLevelPresenter _startLevelPresenter;
    [SerializeField] private EndLevelPresenter _endLevelPresenter;
    [SerializeField] private GameLoop _gameLoop;
    [Space(30f)]
    [SerializeField] private GameEventMediator _gameEventMediator;

    private PlanetPool _planetPool;
    private EntityPool<Energy> _energyPool;
    private CollisionSplashPool _collisionSplashPool;

    private PlanetSpawner _planetSpawner;

    private PauseService _pauseService;
    private PlayerDataService _playerDataService;
    private PlayerDataSystem _playerDataSystem;

    private LevelConditions _levelConditions;
    private LevelPrepareSystem _levelPrepareSystem;

    private AdvertisingService _advertisingService;

    private PrepareLevelState _prepareLevelState;
    private StartLevelState _startLevelState;
    private EndLevelState _endLevelState;
    private LevelStates _levelStates;


    private void Awake()
    {
        InitializePools();
        InitializeSpawners();

        InitializeAudio();
        InitializeData();
        InitializeInput();
        InitializePause();
        InitializeSDK();

        InitializeGameplay();
        InitializeUi();
        InitializeTutorial();

        InitializeGameLoop();

        _gameEventMediator.Initialize(_planetPool, _gameOverHandler, _gameLoop, _gameUI, _planetLauncher);
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

    private void InitializeAudio()
    {
        _audioHandler.Initialize(_audioService, _gameEventMediator);
        _muteController.Initialize(_audioService);
    }

    private void InitializeData()
    {
        _playerDataService = new PlayerDataService();
        _playerDataSystem = new PlayerDataSystem(_playerDataService, _leaderboard);
    }

    private void InitializeInput()
    {
        _inputController.Initialize(_playerInput, _gameEventMediator);
    }

    private void InitializePause()
    {
        _pauseService = new PauseService(_audioService);
    }

    private void InitializeSDK()
    {
        _advertisingService = new AdvertisingService(_pauseService, _rewardHandler);
        _focusController.Initialize(_pauseService, _advertisingService);
    }


    private void InitializeGameplay()
    {
        _levelPlanetsController.Initialize(_gameEventMediator);
        _energyLimitController.Initialize(_gameEventMediator);
        _levelGoalController.Initialize(_gameEventMediator);
        _gameOverHandler.Initialize(_energyLimitController, _levelGoalController);
        _rewardHandler.Initialize(_energyLimitController);

        _planetLauncher.Initialize(_playerInput, _planetSpawner, _energyLimitController);

        _levelConditions = new LevelConditions(_levelGoalController, _energyLimitController);
        _levelGenerator.Initialize(_planetSpawner, _levelConditions, _planetLauncher);
        _levelPrepareSystem = new LevelPrepareSystem(_levelGenerator, _levelPlanetsController, _gameUI);
    }

    private void InitializeUi()
    {
        _uiPanel.Initialize(_energyLimitController, _levelGoalController);
        _gameUI.Initialize(_uiPanel);
    }


    private void InitializeTutorial()
    {
        _tutorialSystem.Initialize(_inputController, _gameEventMediator);
    }

    private void InitializeGameLoop()
    {
        IReadOnlyPlayerData playerData = _playerDataService.PlayerData;

        _prepareLevelState = new(playerData, _levelPrepareSystem);
        _startLevelState = new(playerData, _tutorialSystem, _startLevelPresenter);
        _endLevelState = new(_endLevelPresenter, _playerDataSystem, _levelPlanetsController);
        _levelStates = new(_prepareLevelState, _startLevelState, _endLevelState);

        _gameLoop.Initialize(_gameEventMediator, _levelStates, _advertisingService);
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
