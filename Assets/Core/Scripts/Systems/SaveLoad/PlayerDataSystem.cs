using PlanetMerge.SDK.Yandex;

namespace PlanetMerge.Systems.SaveLoad
{
    public class PlayerDataSystem
    {
        private readonly PlayerDataService _playerDataService;
        private readonly YandexLeaderboard _leaderboard;
        private readonly IReadOnlyPlayerData _playerData;

        public PlayerDataSystem(PlayerDataService playerDataService, YandexLeaderboard leaderboard)
        {
            _playerDataService = playerDataService;
            _leaderboard = leaderboard;

            _playerData = _playerDataService.PlayerData;
        }

        public IReadOnlyPlayerData PlayerData => _playerData;

  
        public void UpdatePlayerData(int levelScore)
        {
            _playerDataService.LevelUp();
            _playerDataService.AddScore(levelScore);

            if (_playerData.Level % Constants.PlanetUpgradeStep == 0)
            {
                _playerDataService.UpgradePlanetRank();
            }

#if UNITY_WEBGL && !UNITY_EDITOR
        _leaderboard.SetPlayerScore(_playerData.Score);
#endif
        }
    }
}