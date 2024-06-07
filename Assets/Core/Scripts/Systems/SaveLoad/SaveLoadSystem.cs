using PlanetMerge.Data;
using UnityEngine;
#if UNITY_WEBGL && !UNITY_EDITOR
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;
#endif

namespace PlanetMerge.Systems.SaveLoad
{
    public class SaveLoadSystem
    {
        private const string LevelKey = nameof(LevelKey);
        private const string ScoreKey = nameof(ScoreKey);
        private const string PlanetRankKey = nameof(PlanetRankKey);

        public PlayerData Load()
        {
            int level = PlayerPrefs.GetInt(LevelKey, Constants.MinimalLevel);
            int planetRank = PlayerPrefs.GetInt(PlanetRankKey, Constants.MinimalPlanetRank);
            int score = PlayerPrefs.GetInt(ScoreKey);

            return new PlayerData(level, planetRank, score);
        }

        public void Save(PlayerData playerData)
        {
            PlayerPrefs.SetInt(LevelKey, playerData.Level);
            PlayerPrefs.SetInt(PlanetRankKey, playerData.PlanetRank);
            PlayerPrefs.SetInt(ScoreKey, playerData.Score);

            PlayerPrefs.Save();
        }

        public void Reset()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}