public class PrepareLevelHandler
{
    private readonly LevelPreparer _levelPreparer;
    private readonly IReadOnlyPlayerData _playerData;

    public PrepareLevelHandler(IReadOnlyPlayerData playerData,LevelPreparer levelPreparer)
    {
        _levelPreparer = levelPreparer;
        _playerData = playerData;
    }

    public void PrepareLevel()
    {
        _levelPreparer.Prepare(_playerData);
    }
}
