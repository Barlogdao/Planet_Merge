using UnityEngine;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelLayoutService", menuName = "Configs/Services/LevelLayoutService", order = 1)]
    public class LevelLayoutService : ScriptableObject
    {
        [SerializeField] private LevelLayout[] _levelLayouts;

        [field: SerializeField] public LevelLayout TutorialLevelLayout { get; private set; }

        public LevelLayout GetLevelLayout(int level)
        {
            if (level == Constants.TutorialLevel)
            {
                return TutorialLevelLayout;
            }

            int index = Random.Range(0, _levelLayouts.Length);

            return _levelLayouts[index];
        }
    }
}