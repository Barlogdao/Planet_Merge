public class ScoreHandler
{
    private LevelPlanetsController _levelPlanets;

    public ScoreHandler(LevelPlanetsController levelPlanets)
    {
        _levelPlanets = levelPlanets;
    }

    public int GetScore()
    {
        int score = 0;

        foreach (var planet in _levelPlanets.Planets)
        {
            score += planet.Rank * planet.Rank;
        }

        return score;
    }
}
