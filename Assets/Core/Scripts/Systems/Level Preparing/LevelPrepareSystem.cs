using PlanetMerge.Systems;
using PlanetMerge.UI;

public class LevelPrepareSystem
{
    private LevelGenerator _levelGenerator;
    private LevelPlanetsController _levelPlanets;
    private GameUi _gameUI;

    public LevelPrepareSystem(LevelGenerator levelGenerator, LevelPlanetsController levelPlanets, GameUi gameUI)
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
