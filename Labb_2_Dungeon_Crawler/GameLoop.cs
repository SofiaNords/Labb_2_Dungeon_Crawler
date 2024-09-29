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

                if (IsValidMove(newX, newY))
                {
                    player.PositionX = newX;
                    player.PositionY = newY;
                }

                DrawLevel();
            }
        }
    }

    private bool IsValidMove(int x, int y)
    {
        foreach (var element in _levelData.Elements)
        {
            if (element.PositionX == x && element.PositionY == y && element is Wall)
            {
                return false;
            }
        }
        return true;
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