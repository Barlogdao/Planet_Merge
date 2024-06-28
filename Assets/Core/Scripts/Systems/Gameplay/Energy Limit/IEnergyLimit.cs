namespace PlanetMerge.Systems.Gameplay
{
    public interface IEnergyLimit : IEnergyLimitNotifier
    {
        bool HasEnergy { get; }

        bool TrySpendEnergy();
    }
}