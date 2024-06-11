namespace PlanetMerge.Systems.Gameplay
{
    public class LevelScore
    {
        private LevelPlanetsController _levelPlanetsController;

        public LevelScore(LevelPlanetsController levelPlanets)
        {
            _levelPlanetsController = levelPlanets;
        }

        public int Get()
        {
            int score = 0;

            foreach (var planet in _levelPlanetsController.Planets)
            {
                score += planet.Rank * planet.Rank;
            }

            return score;
        }
    }
}