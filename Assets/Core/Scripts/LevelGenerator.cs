using PlanetMerge.Configs;
using PlanetMerge.Data;
using PlanetMerge.Planets;
using System;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelGoalService _levelGoalService;
        [SerializeField] private LevelLayoutService _levelLayoutService;
        [SerializeField] private int _limitAmount = 3;

        private PlayerData _playerData;
        private LevelGoalHandler _levelGoalHandler;
        private PlanetLimitHandler _planetLimitHandler;
        private PlanetLauncher _planetLauncher;
        private PlanetFactory _planetFactory;

        public event Action LevelGenerated; 

        public void Initialize(PlanetFactory planetFactory, PlayerData playerData, LevelGoalHandler levelGoalHandler, PlanetLimitHandler planetLimitHandler, PlanetLauncher planetLauncher)
        {
            _planetFactory = planetFactory;
            _playerData = playerData;
            _levelGoalHandler = levelGoalHandler;
            _planetLimitHandler = planetLimitHandler;
            _planetLauncher = planetLauncher;
        }

        public void Generate()
        {
            LevelGoal levelGoal = _levelGoalService.GetLevelGoal();
            LevelLayout levelLayout = _levelLayoutService.GetLevelLayout();

            int planetRank = _playerData.PlanetRank;

            SetGoal(levelGoal, planetRank);
            SetPlanets(levelLayout, planetRank);
            SetPlanetLauncher(planetRank);

            LevelGenerated?.Invoke();
        }

        private void SetGoal(LevelGoal levelGoal, int planetRank)
        {
            _levelGoalHandler.Prepare(levelGoal.MergeAmount, planetRank + levelGoal.PlanetRankModifier);
        }

        private void SetPlanets(LevelLayout levelLayout, int planetRank)
        {
            foreach (PlanetSetup planetSetup in levelLayout.PlanetSetups)
            {
                _planetFactory.Create(planetSetup.Position, planetRank + planetSetup.RankModifier);
            }
        }

        private void SetPlanetLauncher(int planetRank)
        {
            _planetLauncher.Prepare(planetRank, _limitAmount);
        }
    }
}