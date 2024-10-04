public class GameData
{
    private LevelData _levelData;
    private Player _player;

    public GameData(LevelData levelData)
    {
        _levelData = levelData;
        _player = _levelData.StartPlayer;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {_player.Name} - HP: {_player.HP}");
    }

}