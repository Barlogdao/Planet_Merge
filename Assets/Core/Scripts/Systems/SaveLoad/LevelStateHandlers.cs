using System.Collections;
public class LevelStateHandlers
{
    public LevelStateHandlers(PrepareLevelHandler prepareLevelHandler, StartLevelHandler startLevelHandler, EndLevelHandler endLevelHandler)
    {
        PrepareLevelHandler = prepareLevelHandler;
        StartLevelHandler = startLevelHandler;
        EndLevelHandler = endLevelHandler;
    }

    public PrepareLevelHandler PrepareLevelHandler { get; }
    public StartLevelHandler StartLevelHandler { get; }
    public EndLevelHandler EndLevelHandler { get; }
}
