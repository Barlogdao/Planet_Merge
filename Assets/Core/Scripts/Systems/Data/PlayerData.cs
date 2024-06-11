namespace PlanetMerge.Systems.Data
{
    public class PlayerData : IReadOnlyPlayerData
    {
        public PlayerData(int level, int planetRank, int score)
        {
            Level = level;
            PlanetRank = planetRank;
            Score = score;
        }

        public int Level { get;  set; }
        public int PlanetRank { get;  set; }
        public int Score { get; set; }
    }
}