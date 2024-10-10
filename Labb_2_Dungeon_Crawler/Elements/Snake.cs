public class Snake : Enemy
{
    private LevelData _levelData;

    public Snake(int x, int y, LevelData levelData)
    {
        ClassChar = 's';
        Color = ConsoleColor.Green;
        PositionX = x;
        PositionY = y;
        Name = "Snake";
        HP = 25;
        _levelData = levelData;
        AttackDice = new Dice(3, 4, 2);
        DefenceDice = new Dice(1, 8, 5);
    }

    public override void Update(Player player)
    {
        int playerPositionX = player.PositionX;
        int playerPositionY = player.PositionY;

        int distanceX = Math.Abs(PositionX - playerPositionX);
        int distanceY = Math.Abs(PositionY - playerPositionY);

        int newX = PositionX;
        int newY = PositionY;

        if (distanceX <= 2 && distanceY <= 2)
        {
            if (PositionX < playerPositionX)
            {
                newX--;
            }
            else
            {
                newX++;
            }

            if (PositionY < playerPositionY)
            {
                newY--;
            }
            else
            {
                newY++;
            }
        }

        if (player.IsValidMove(newX, newY, _levelData))
        {
            PositionX = newX;
            PositionY = newY;
        }
    }
}
