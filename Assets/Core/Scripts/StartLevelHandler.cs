using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.Tutorial;
using PlanetMerge.UI;

public class StartLevelHandler
{
    private GameUi _gameUi;
    private LevelPreparer _levelPreparer;
    private TutorialSystem _tutorialSystem;

    public StartLevelHandler(GameUi gameUi, LevelPreparer levelPreparer, TutorialSystem tutorialController)
    {
        _gameUi = gameUi;
        _levelPreparer = levelPreparer;
        _tutorialSystem = tutorialController;
    }

    public void PrepareLevel(IReadOnlyPlayerData playerData)
    {
        _levelPreparer.Prepare(playerData);
    }

    public async UniTask StartLevelAsync(int level)
    {
        _gameUi.Hide();

        if (level == Constants.TutorialLevel)
        {
            _tutorialSystem.RunTutorialAsync().Forget();
        }
        else
        {
            await _gameUi.RunLevelAsync();
        }
    }

    public void ResumeLevel()
    {
        _gameUi.Hide();
    }
}
