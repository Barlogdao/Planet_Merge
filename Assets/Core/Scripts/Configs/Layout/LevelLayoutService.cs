using NaughtyAttributes;
using UnityEngine;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelLayoutService", menuName = "Configs/Services/LevelLayoutService", order = 1)]
    public class LevelLayoutService : ScriptableObject
    {
        private const int IndexOffset = 1;

        [SerializeField, Expandable] private LevelLayout[] _levelLayouts;

        [field: SerializeField] public LevelLayout TutorialLevelLayout { get; private set; }

        public LevelLayout GetLevelLayout(int level)
        {
            int index;

            if (level <= _levelLayouts.Length)
            {
                index = level - IndexOffset;
            }
            else
            {
                index = Random.Range(0, _levelLayouts.Length);
            }

            return _levelLayouts[index];
        }
    }
}