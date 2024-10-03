
public class GameLoop
{
    private LevelData _levelData;
    private Dictionary<LevelElement, (int, int)> _previousPositions = new Dictionary<LevelElement, (int, int)>();

    public GameLoop(LevelData levelData)
    {
        _levelData = levelData;
    }

    public void Run()
    {
        Player player = _levelData.StartPlayer;
        Rat rat = _levelData.Rat;
        int turn = 0;

        Console.CursorVisible = false;

        InitializeElementPositions();

        //Console.WriteLine($"Name: {player.Name} - Health: {player.HP} - Turn: {turn}");

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
                    //int playerAttackPoints = player.AttackEnemy();
                    int playerAttackPoints = player.AttackDice.Throw();
                    //int ratDefencePoints = rat.Defence(); 
                    int ratDefencePoints = rat.DefenceDice.Throw();

                    int attackScore = playerAttackPoints - ratDefencePoints;

                    if (attackScore > 0)
                    {
                        rat.HP = rat.HP - attackScore;
                    }
                    else if (attackScore < 0)
                    {
                        player.HP = player.HP + attackScore;
                    }
                }
                UpdateElementPositions();

                if (IsValidMove(newX, newY))
                {
                    player.PositionX = newX;
                    player.PositionY = newY;
                }

                foreach (var element in _levelData.Elements)
                {
                    if (element is Enemy enemy)
                    {
                        enemy.Update(player);
                    }
                }
                turn++;
                DrawLevel();
            }
        }
    }

    private void InitializeElementPositions()
    {
        foreach(var element in _levelData.Elements)
        {
            _previousPositions[element] = (element.PositionX, element.PositionY);
        }
    }

    private void UpdateElementPositions()
    {
        foreach(var element in _levelData.Elements)
        {
            var previousPos = _previousPositions[element];
            if (element.PositionX != previousPos.Item1 || element.PositionY != previousPos.Item2)
            {
                _previousPositions[element] = (element.PositionX, element.PositionY);
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
        foreach (LevelElement element in _levelData.Elements)
        {
            var previousPos = _previousPositions[element];
            if (element.PositionX != previousPos.Item1 || element.PositionY != previousPos.Item2)
            {
                Console.SetCursorPosition(previousPos.Item1, previousPos.Item2);
                Console.Write(" ");

                Console.SetCursorPosition(element.PositionX, element.PositionY);
                element.Draw();
            }
        }
    }
}