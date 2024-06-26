using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.Gameplay.LevelPreparing;

namespace PlanetMerge.Loop.States
{
    public class PrepareLevelState
    {
        private readonly LevelPrepareSystem _levelPrepareSystem;
        private readonly IReadOnlyPlayerData _playerData;

        public PrepareLevelState(IReadOnlyPlayerData playerData, LevelPrepareSystem levelPrepareSystem)
        {
            _levelPrepareSystem = levelPrepareSystem;
            _playerData = playerData;
        }

        public void PrepareLevel()
        {
            _levelPrepareSystem.Prepare(_playerData);
        }
    }
}