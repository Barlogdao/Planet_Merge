using PlanetMerge.Configs;
using PlanetMerge.Planets;
using PlanetMerge.UI;
using System;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelGoalService _levelGoalService;
        [SerializeField] private LevelLayoutService _levelLayoutService;
        [SerializeField] private LevelLimitService _levelLimitService;

        private PlanetLauncher _planetLauncher;
        private PlanetSpawner _planetSpawner;
        private GameUI _gameUI;
        private LevelConditions _levelConditions;

        public event Action LevelCreated;

        public void Initialize(PlanetSpawner planetSpawner, LevelConditions levelConditions, PlanetLauncher planetLauncher, GameUI gameUI)
        {
            _planetSpawner = planetSpawner;
            _levelConditions = levelConditions;
            _planetLauncher = planetLauncher;
            _gameUI = gameUI;
        }

        public void Generate(IReadOnlyPlayerData playerData)
        {
            LevelGoal levelGoal = _levelGoalService.GetLevelGoal();
            LevelLayout levelLayout = _levelLayoutService.GetLevelLayout();
            int limitAmount = _levelLimitService.GetLimitAmount();

            int planetRank = playerData.PlanetRank;

            SetPlanets(levelLayout, planetRank);
            SetPlanetLauncher(planetRank);

            SetLevelConditions(levelGoal, planetRank, limitAmount);
            PrepareUI(playerData);

            LevelCreated?.Invoke();
        }

        private void SetLevelConditions(LevelGoal levelGoal, int planetRank, int limitAmount)
        {
            _levelConditions.Prepare(levelGoal, planetRank, limitAmount);
        }

        private void PrepareUI(IReadOnlyPlayerData playerData)
        {
            _gameUI.Prepare(playerData);
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