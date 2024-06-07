using PlanetMerge.Systems;
using PlanetMerge.UI;

public class LevelPrepareSystem
{
    private readonly LevelGenerator _levelGenerator;
    private readonly LevelPlanetsController _levelPlanetsController;
    private readonly GameUi _gameUI;

    public LevelPrepareSystem(LevelGenerator levelGenerator, LevelPlanetsController levelPlanetsController, GameUi gameUI)
    {
        _levelGenerator = levelGenerator;
        _levelPlanetsController = levelPlanetsController;
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
        _levelPlanetsController.Clear();
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
