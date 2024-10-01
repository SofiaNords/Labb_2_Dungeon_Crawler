﻿
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
        Rat rat = _levelData.Rat;

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

                if (IsAttackMove(newX, newY))
                {
                    player.AttackEnemy();
                    rat.Defence();
                }

                if (IsValidMove(newX, newY))
                {
                    player.PositionX = newX;
                    player.PositionY = newY;
                }

                foreach (var element in _levelData.Elements)
                {
                    if (element is Enemy enemy)
                    {
                        enemy.Update();
                    }
                }

                DrawLevel();
            }
        }
    }

    private bool IsValidMove(int x, int y)
    {
        foreach (var element in _levelData.Elements)
        {
            if (element.PositionX == x && element.PositionY == y && (element is Wall || element is Enemy))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsAttackMove(int x, int y)
    {
        foreach (var element in _levelData.Elements)
        {
            if (element.PositionX == x && element.PositionY == y && (element is Enemy))
            {
                return true;
            }
        }
        return false;
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