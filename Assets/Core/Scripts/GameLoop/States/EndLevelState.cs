using Cysharp.Threading.Tasks;
using PlanetMerge.Gameloop.Presenter;
using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.Gameplay;

namespace PlanetMerge.Gameloop.States
{
    public class EndLevelState
    {
        private readonly EndLevelPresenter _endLevelPresenter;
        private readonly PlayerDataSystem _playerDataSystem;
        private readonly LevelPlanetsController _levelPlanetsController;
        private readonly IReadOnlyPlayerData _playerData;
        private readonly LevelScore _levelScore;

        public EndLevelState(EndLevelPresenter endLevelPresenter,
            PlayerDataSystem playerDataSystem,
            LevelPlanetsController levelPlanetsController)
        {
            _endLevelPresenter = endLevelPresenter;
            _playerDataSystem = playerDataSystem;
            _levelPlanetsController = levelPlanetsController;
            _levelScore = new LevelScore(_levelPlanetsController);

            _playerData = _playerDataSystem.PlayerData;
        }

        public async UniTask Win()
        {
            int levelScore = _levelScore.Get();
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
}