public class ScoreHandler
{
    private LevelPlanetsController _levelPlanetsController;

    public ScoreHandler(LevelPlanetsController levelPlanets)
    {
        _levelPlanetsController = levelPlanets;
    }

    public int GetScore()
    {
        int score = 0;

        foreach (var planet in _levelPlanetsController.Planets)
        {
            score += planet.Rank * planet.Rank;
        }

        return score;
    }
}
