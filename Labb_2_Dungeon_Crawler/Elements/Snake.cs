public class Snake : Enemy
{
    private LevelData _levelData;
    private Player _player;

    public Snake(int x, int y, LevelData levelData, Player player)
    {
        ClassChar = 's';
        Color = ConsoleColor.Green;
        PositionX = x;
        PositionY = y;
        Name = "Snake";
        HP = 25;
        _levelData = levelData;
        _player = player;
    }

    public override void Update()
    {
        int playerPositionX = _player.PositionX;
        int playerPositionY = _player.PositionY;

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

        if (IsValidMove(newX, newY))
        {
            PositionX = newX;
            PositionY = newY;
        }
    }

    private bool IsValidMove(int x, int y)
    {
        foreach (var element in _levelData.Elements)
        {
            if (element.PositionX == x && element.PositionY == y && (element is Wall || element is Enemy || element is Player))
            {
                return false;
            }
        }
        return true;
    }
}
