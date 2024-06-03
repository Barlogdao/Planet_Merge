using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.Tutorial;
using PlanetMerge.Systems.Visual;
using PlanetMerge.UI;

public class StartLevelHandler
{
    private GameUi _gameUi;
    private LevelPreparer _levelPreparer;
    private TutorialSystem _tutorialSystem;
    private StartLevelViewController _startLevelViewController;
    

    public StartLevelHandler(GameUi gameUi, LevelPreparer levelPreparer, TutorialSystem tutorialController, StartLevelViewController startLevelViewController)
    {
        _gameUi = gameUi;
        _levelPreparer = levelPreparer;
        _tutorialSystem = tutorialController;
        _startLevelViewController = startLevelViewController;
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
            await _startLevelViewController.StartLevelAppear();
        }
    }

    public void ResumeLevel()
    {
        _gameUi.Hide();
    }
}
