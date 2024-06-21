using PlanetMerge.Configs.Goals;
using PlanetMerge.Configs.Layouts;
using PlanetMerge.Configs.Limits;
using PlanetMerge.Spawners;
using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.Gameplay.PlanetLaunching;
using UnityEngine;

namespace PlanetMerge.Systems.Gameplay.LevelPreparing
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelGoalService _levelGoalService;
        [SerializeField] private LevelLayoutService _levelLayoutService;
        [SerializeField] private LevelLimitService _levelLimitService;

        private PlanetLauncher _planetLauncher;
        private PlanetSpawner _planetSpawner;
        private LevelConditions _levelConditions;

        public void Initialize(
            PlanetSpawner planetSpawner,
            LevelConditions levelConditions,
            PlanetLauncher planetLauncher)
        {
            _planetSpawner = planetSpawner;
            _levelConditions = levelConditions;
            _planetLauncher = planetLauncher;
        }

        public void Generate(IReadOnlyPlayerData playerData)
        {
            int level = playerData.Level;
            LevelGoal levelGoal = _levelGoalService.GetLevelGoal(level);
            LevelLayout levelLayout = _levelLayoutService.GetLevelLayout(level);
            int limitAmount = _levelLimitService.GetLimitAmount();
            int planetRank = playerData.PlanetRank;

            SetPlanets(levelLayout, planetRank);
            SetPlanetLauncher(planetRank);
            SetLevelConditions(levelGoal, planetRank, limitAmount);
        }

        private void SetLevelConditions(LevelGoal levelGoal, int planetRank, int limitAmount)
        {
            _levelConditions.Prepare(levelGoal, planetRank, limitAmount);
        }

        private void SetPlanets(LevelLayout levelLayout, int planetRank)
        {
            foreach (PlanetSetup planetSetup in levelLayout.PlanetSetups)
            {
                _planetSpawner.Spawn(planetSetup.Position, planetRank + planetSetup.RankModifier);
            }
        }

        private void SetPlanetLauncher(int planetRank)
        {
            _planetLauncher.Prepare(planetRank);
        }
    }
}