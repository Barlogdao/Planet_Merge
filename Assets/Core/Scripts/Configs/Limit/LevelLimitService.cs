using UnityEngine;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelLimitService", menuName = "Configs/Services/LevelLimitService", order = 1)]
    public class LevelLimitService : ScriptableObject
    {
        [SerializeField] private int _limit;

        public int GetLimitAmount()
        {
            return _limit;
        }
    }
}