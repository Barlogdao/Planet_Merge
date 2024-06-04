using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.SaveLoad;

public class EndLevelHandler
{
    private readonly EndLevelPresenter _endLevelPresenter;
    private readonly IReadOnlyPlayerData _playerData;
    private readonly PlayerDataSystem _playerDataSystem;

    public EndLevelHandler(IReadOnlyPlayerData playerData, EndLevelPresenter endLevelPresenter, PlayerDataSystem playerDataSystem)
    {
        _endLevelPresenter = endLevelPresenter;
        _playerData = playerData;
        _playerDataSystem = playerDataSystem;
    }

    public async UniTask Win()
    {
        int levelScore = _playerDataSystem.GetLevelScore();
        int currentPlanetRank = _playerData.PlanetRank;

        _playerDataSystem.UpdatePlayerData();

        await _endLevelPresenter.ShowWinAsync(levelScore, currentPlanetRank, _playerData);
    }

    public void Loose()
    {
        _endLevelPresenter.ShowLooseAsync().Forget();
    }
}
