using System.Linq.Expressions;
using System.Runtime.CompilerServices;

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

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                int newX = player.PositionX;
                int newY = player.PositionY;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        newY--;
                        break;
                    case ConsoleKey.DownArrow:
                        newY++;
                        break;
                    case ConsoleKey.LeftArrow:
                        newX--;
                        break;
                    case ConsoleKey.RightArrow:
                        newX++;
                        break;
                }

                player.PositionX = newX;
                player.PositionY = newY;

                DrawLevel();
            }
        }
    }

    private void DrawLevel()
    {
        Console.Clear();
        foreach (LevelElement element in _levelData.Elements)
        {
            element.Draw();
        }
    }
}