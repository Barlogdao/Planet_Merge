using PlanetMerge.Planets;
using UnityEngine;

namespace PlanetMerge.Handlers.Split
{
    public class SplitHandler : MonoBehaviour
    {
        private GameEventMediator _gameEventMediator;
        private LevelPlanets _levelPlanets;

        public void Initialize(GameEventMediator gameEventMediator, LevelPlanets levelPlanets)
        {
            _gameEventMediator = gameEventMediator;
            _levelPlanets = levelPlanets;

            _gameEventMediator.GameWon += OnGameWon;
        }

        private void OnDestroy()
        {
            _gameEventMediator.GameWon -= OnGameWon;
        }

        private void OnGameWon()
        {
            foreach (Planet planet in _levelPlanets.Planets)
            {
                planet.Split();
            }
        }
    }
}