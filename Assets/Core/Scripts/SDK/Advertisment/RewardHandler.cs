using UnityEngine;

namespace PlanetMerge.Systems.Gameplay
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private int _rewardEnergyAmount = 5;

        private EnergyLimitController _energyLimitHandler;

        public void Initialize(EnergyLimitController energyLimitHandler)
        {
            _energyLimitHandler = energyLimitHandler;
        }

        public void GetReward()
        {
            _energyLimitHandler.SetLimit(_rewardEnergyAmount);
        }
    }
}