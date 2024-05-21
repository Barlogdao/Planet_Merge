using PlanetMerge.Systems;
using PlanetMerge.UI;

public class LevelPreparer
{
    private LevelGenerator _levelGenerator;
    private LevelPlanets _levelPlanets;
    private GameUi _gameUI;

    public LevelPreparer(LevelGenerator levelGenerator, LevelPlanets levelPlanets, GameUi gameUI)
    {
        _levelGenerator = levelGenerator;
        _levelPlanets = levelPlanets;
        _gameUI = gameUI;
    }

    public void Prepare(IReadOnlyPlayerData playerData)
    {
        ClearLevel();

        GenerateLevel(playerData);
        PrepareUI(playerData);
    }

    private void ClearLevel()
    {
        _levelPlanets.Clear();
        _gameUI.Hide();
    }

    private void GenerateLevel(IReadOnlyPlayerData playerData)
    {
        _levelGenerator.Generate(playerData);
    }

    private void PrepareUI(IReadOnlyPlayerData playerData)
    {
        _gameUI.Prepare(playerData);
    }
}
