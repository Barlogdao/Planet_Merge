using UnityEngine;
using NaughtyAttributes;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelLayoutService", menuName = "Configs/Services/LevelLayoutService", order = 1)]
    public class LevelLayoutService : ScriptableObject
    {
        [SerializeField, Expandable] private LevelLayout[] _levelLayouts;

        private readonly int _indexOffset = 1;

        [field: SerializeField] public LevelLayout TutorialLevelLayout { get; private set; }

        public LevelLayout GetLevelLayout(int level)
        {
            int index;

            if (level <= _levelLayouts.Length)
            {
                index = level - _indexOffset;
            }
            else
            {
                index = Random.Range(0, _levelLayouts.Length);
            }

            return _levelLayouts[index];
        }
    }
}