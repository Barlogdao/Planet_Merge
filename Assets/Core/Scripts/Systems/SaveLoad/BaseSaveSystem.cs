using PlanetMerge.Systems.Data;

namespace PlanetMerge.Systems.SaveLoad
{
    public abstract class BaseSaveSystem
    {
        protected const string LevelKey = nameof(LevelKey);
        protected const string ScoreKey = nameof(ScoreKey);
        protected const string PlanetRankKey = nameof(PlanetRankKey);

        public abstract PlayerData Load();

        public abstract void Save(PlayerData playerData);
    }
}