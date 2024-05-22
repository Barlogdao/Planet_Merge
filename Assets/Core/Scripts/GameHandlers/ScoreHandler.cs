using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler
{
    private LevelPlanets _levelPlanets;

    public ScoreHandler(LevelPlanets levelPlanets)
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
