using PlanetMerge.Data;

namespace PlanetMerge.Systems.SaveLoad
{
    public class PlayerDataService
    {
        private readonly SaveLoadSystem _saveLoadSystem;
        private readonly PlayerData _playerData;

        public PlayerDataService()
        {
            _saveLoadSystem = new SaveLoadSystem();
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

        public void Reset()
        {
            _saveLoadSystem.Reset();
        }
    }
}