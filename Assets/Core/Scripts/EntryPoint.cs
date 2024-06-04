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
    [SerializeField] private EnergyLimitHandler _energyLimitHandler;
    [SerializeField] private GameEventMediator _gameEventMediator;

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private GameOverHandler _gameOverHandler;
    [SerializeField] private GameUi _gameUI;

    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private LevelPlanets _levelPlanets;
    [SerializeField] private InputController _inputController;

    [SerializeField] private RewardHandler _rewardHandler;
    private InterstitialHandler _interstitialHandler;

    [SerializeField] private UiPanel _uiPanel;
    [SerializeField] private SplitHandler _splitHandler;

    [SerializeField] private AudioService _audioService;
    [SerializeField] private AudioHandler _audioHandler;
    [SerializeField] private MuteHandler _muteHandler;
    [SerializeField] private FocusHandler _focusHandler;
    [SerializeField] private TutorialSystem _tutorialSystem;
    [SerializeField] private EnergyCollectSystem _energyCollectSystem;
    [SerializeField] private CollisionSplashSystem _collisionSplashSystem;
    [SerializeField] private EndLevelPresenter _endLevelPresenter;

    [SerializeField] private YandexLeaderboard _leaderboard;

    [SerializeField] private StartLevelPresenter _startLevelPresenter;

    private PlanetSpawner _planetSpawner;


    private PlanetPool _planetPool;
    private EntityPool<Energy> _energyPool;
    private CollisionSplashPool _collisionSplashPool;

    private EnergyLimit _energyLimit;
    private LevelConditions _levelConditions;
    private LevelPreparer _levelPreparer;
  
    private PlayerDataService _playerDataService;
    private ScoreHandler _scoreHandler;
    private PauseService _pauseService;
    private PlayerDataSystem _playerDataSystem;

    private void Awake()
    {
        _inputController.Initialize(_playerInput, _gameEventMediator);


        InitializeServices();
        InitializePools();
        InitializeSpawners();


        InitializePlanetSystem();
        InitializePlanetLauncher();

        InitializeHandlers();

        InitializeUi();
        InitializeLevelPreparer();
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
        _energyCollectSystem.Initialize(_gameEventMediator, _planetLauncher, _energyPool);
        _collisionSplashSystem.Initialize(_gameEventMediator, _collisionSplashPool);
    }

    private void InitializePlanetSystem()
    {

        _energyLimit = new EnergyLimit();
        _levelPlanets.Initialize(_gameEventMediator);
    }

    private void InitializeServices()
    {
        _pauseService = new PauseService(_audioService);
    }

    private void InitializePlanetLauncher()
    {
        _planetLauncher.Initialize(_playerInput, _planetSpawner, _energyLimit, _trajectory);
        float planetRadius = _planetPrefab.GetComponent<CircleCollider2D>().radius * _planetPrefab.transform.localScale.x;
        _trajectory.Initialize(_gameEventMediator, _planetLauncher, planetRadius);
    }



    private void InitializeHandlers()
    {
        _tutorialSystem.Initialize(_playerInput);

        _scoreHandler = new ScoreHandler(_levelPlanets);
        _energyLimitHandler.Initialize(_gameEventMediator, _energyLimit);
        _goalHandler.Initialize(_gameEventMediator);
        _gameOverHandler.Initialize(_energyLimitHandler, _goalHandler);

        _focusHandler.Initialize(_pauseService);
        _splitHandler.Initialize(_gameEventMediator, _levelPlanets);
        _audioHandler.Initialize(_audioService, _gameEventMediator);
        _muteHandler.Initialize(_audioService);

        _interstitialHandler = new InterstitialHandler(_pauseService);
        _rewardHandler.Initialize(_energyLimitHandler, _pauseService);
    }

    private void InitializeUi()
    {
        _uiPanel.Initialize(_energyLimitHandler, _goalHandler);
        _gameUI.Initialize(_uiPanel);
    }

    private void InitializeLevelPreparer()
    {
        _levelConditions = new LevelConditions(_goalHandler, _energyLimitHandler);
        _levelGenerator.Initialize(_planetSpawner, _levelConditions, _planetLauncher);
        _levelPreparer = new LevelPreparer(_levelGenerator, _levelPlanets, _gameUI);
    }

    private void InitializeGameLoop()
    {
        _playerDataService = new PlayerDataService();
        IReadOnlyPlayerData playerData = _playerDataService.PlayerData;

        _playerDataSystem = new PlayerDataSystem(_scoreHandler, _playerDataService, _leaderboard);
        PrepareLevelHandler prepareLevelHandler = new (playerData,_levelPreparer);
        StartLevelHandler startLevelHandler = new (playerData, _tutorialSystem, _startLevelPresenter);
        EndLevelHandler endLevelHandler = new (playerData,_endLevelPresenter, _playerDataSystem);
        LevelStateHandlers levelStateHandlers = new (prepareLevelHandler, startLevelHandler, endLevelHandler);

        _gameLoop.Initialize(_gameEventMediator, levelStateHandlers, _rewardHandler, _interstitialHandler);
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
