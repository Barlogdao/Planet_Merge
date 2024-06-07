namespace PlanetMerge.Systems
{
    public interface IEnergyLimit: IEnergyLimitNotifier
    {
        bool HasEnergy { get; }

        bool TrySpendEnergy();
    }
}