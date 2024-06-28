using PlanetMerge.Systems.Data;
using PlanetMerge.Utils;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

namespace PlanetMerge.Systems.SaveLoad
{
    public class YandexSaveSystem : BaseSaveSystem
    {
        public override PlayerData Load()
        {
            int level = PlayerPrefs.GetInt(LevelKey, Constants.MinimalLevel);
            int planetRank = PlayerPrefs.GetInt(PlanetRankKey, Constants.MinimalPlanetRank);
            int score = PlayerPrefs.GetInt(ScoreKey);

            return new PlayerData(level, planetRank, score);
        }

        public override void Save(PlayerData playerData)
        {
            PlayerPrefs.SetInt(LevelKey, playerData.Level);
            PlayerPrefs.SetInt(PlanetRankKey, playerData.PlanetRank);
            PlayerPrefs.SetInt(ScoreKey, playerData.Score);

            PlayerPrefs.Save();
        }
    }
}