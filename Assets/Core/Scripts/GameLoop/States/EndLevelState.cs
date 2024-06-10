using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.SaveLoad;

public class EndLevelState
{
    private readonly EndLevelPresenter _endLevelPresenter;
    private readonly PlayerDataSystem _playerDataSystem;
    private readonly LevelPlanetsController _levelPlanetsController;
    private readonly IReadOnlyPlayerData _playerData;
    private readonly ScoreHandler _scoreHandler;

    public EndLevelState(EndLevelPresenter endLevelPresenter, PlayerDataSystem playerDataSystem, LevelPlanetsController levelPlanetsController)
    {
        _endLevelPresenter = endLevelPresenter;
        _playerDataSystem = playerDataSystem;
        _levelPlanetsController = levelPlanetsController;
        _scoreHandler = new ScoreHandler(_levelPlanetsController);

        _playerData = _playerDataSystem.PlayerData;
    }

    public async UniTask Win()
    {
        int levelScore = _scoreHandler.GetScore();
        int currentPlanetRank = _playerData.PlanetRank;
        _levelPlanetsController.SplitPlanets();

        _playerDataSystem.UpdatePlayerData(levelScore);

        await _endLevelPresenter.ShowWinAsync(levelScore, currentPlanetRank, _playerData);
    }

    public void Loose()
    {
        _endLevelPresenter.ShowLooseAsync().Forget();
    }
}
