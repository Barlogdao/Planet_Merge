using PlanetMerge.Configs;
using PlanetMerge.Data;
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

        private IReadOnlyPlayerData _playerData;
        private LevelGoalHandler _levelGoalHandler;

        private PlanetLauncher _planetLauncher;
        private PlanetSpawner _planetSpawner;
        private GameUI _gameUI;

        public event Action LevelCreated;

        public void Initialize(PlanetSpawner planetSpawner, IReadOnlyPlayerData playerData, LevelGoalHandler levelGoalHandler, PlanetLauncher planetLauncher, GameUI gameUI)
        {
            _planetSpawner = planetSpawner;
            _playerData = playerData;
            _levelGoalHandler = levelGoalHandler;
            _gameUI = gameUI;

            _planetLauncher = planetLauncher;
        }

        public void Generate()
        {
            LevelGoal levelGoal = _levelGoalService.GetLevelGoal();
            LevelLayout levelLayout = _levelLayoutService.GetLevelLayout();
            int limitAmount = _levelLimitService.GetLimitAmount();

            int planetRank = _playerData.PlanetRank;

            SetGoal(levelGoal, planetRank);
            SetPlanets(levelLayout, planetRank);
            SetPlanetLauncher(planetRank, limitAmount);
            PrepareUI();

            LevelCreated?.Invoke();
        }

        private void PrepareUI()
        {
            _gameUI.Prepare(_playerData);
        }

        private void SetGoal(LevelGoal levelGoal, int planetRank)
        {
            _levelGoalHandler.Prepare(levelGoal.MergeAmount, planetRank + levelGoal.PlanetRankModifier);
        }

        private void SetPlanets(LevelLayout levelLayout, int planetRank)
        {
            foreach (PlanetSetup planetSetup in levelLayout.PlanetSetups)
            {
                _planetSpawner.Spawn(planetSetup.Position, planetRank + planetSetup.RankModifier);
            }
        }

        private void SetPlanetLauncher(int planetRank, int limitAmount)
        {
            _planetLauncher.Prepare(planetRank, limitAmount);
        }
    }
}