using NaughtyAttributes;
using UnityEngine;

namespace PlanetMerge.Configs.Layouts
{
    [CreateAssetMenu(fileName = "LevelLayoutService", menuName = "Configs/Services/LevelLayoutService", order = 1)]
    public class LevelLayoutService : ScriptableObject
    {
        private const int IndexOffset = 1;

        [SerializeField, Expandable] private LevelLayout[] _levelLayouts;

        [field: SerializeField] public LevelLayout TutorialLevelLayout { get; private set; }

        public LevelLayout GetLevelLayout(int level)
        {
            int index = (level - IndexOffset) % _levelLayouts.Length;

            return _levelLayouts[index];
        }
    }
}