using Cysharp.Threading.Tasks;
using PlanetMerge.Loop.View;
using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.Gameplay;

namespace PlanetMerge.Loop.States
{
    public class EndLevelState
    {
        private readonly EndLevelView _endLevelView;
        private readonly PlayerDataSystem _playerDataSystem;
        private readonly LevelPlanetsController _levelPlanetsController;
        private readonly IReadOnlyPlayerData _playerData;
        private readonly LevelScore _levelScore;

        public EndLevelState(
            EndLevelView endLevelPresenter,
            PlayerDataSystem playerDataSystem,
            LevelPlanetsController levelPlanetsController)
        {
            _endLevelView = endLevelPresenter;
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

            await _endLevelView.ShowWinAsync(levelScore, currentPlanetRank, _playerData);
        }

        public void Loose()
        {
            _endLevelView.ShowLooseAsync().Forget();
        }
    }
}