using PlanetMerge.Systems.SaveLoad;

namespace PlanetMerge.Systems.Data
{
    public class PlayerDataService
    {
        private readonly BaseSaveSystem _saveLoadSystem;
        private readonly PlayerData _playerData;

        public PlayerDataService()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _saveLoadSystem = new YandexSaveSystem();
        
#else
            _saveLoadSystem = new PlayerPrefsSaveSystem();
#endif
            _playerData = _saveLoadSystem.Load();
        }

        public IReadOnlyPlayerData PlayerData => _playerData;

        public void LevelUp()
        {
            _playerData.Level++;
            Save();
        }

        public void UpgradePlanetRank()
        {
            _playerData.PlanetRank++;
            Save();
        }

        public void AddScore(int score)
        {
            _playerData.Score += score;
            Save();
        }

        private void Save()
        {
            _saveLoadSystem.Save(_playerData);
        }
    }
}