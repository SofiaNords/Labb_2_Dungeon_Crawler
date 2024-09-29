public class GameLoop
{
    private LevelData _levelData;

    public GameLoop(LevelData levelData)
    {
        _levelData = levelData;
    }

    public void Run()
    {
        Player player = _levelData.StartPlayer;
    }
}