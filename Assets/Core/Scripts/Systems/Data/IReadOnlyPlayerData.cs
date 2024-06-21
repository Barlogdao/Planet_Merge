namespace PlanetMerge.Systems.Data
{
    public interface IReadOnlyPlayerData
    {
        int Level { get; }

        int PlanetRank { get; }

        int Score { get; }
    }
}