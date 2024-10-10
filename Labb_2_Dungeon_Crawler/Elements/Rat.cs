public class Rat : Enemy
{
    private LevelData _levelData;
    public Rat(int x, int y, LevelData levelData)
    {
        ClassChar = 'r';
        Color = ConsoleColor.Red;
        PositionX = x;
        PositionY = y;
        Name = "Rat";
        HP = 10;
        _levelData = levelData;
        AttackDice = new Dice(1, 6, 3);
        DefenceDice = new Dice(1, 6, 1);
    }

    public enum Direction
    {
        up,
        down,
        left,
        right,
    }

    public override void Update(Player player)
    {
        int direction;
        int newX = this.PositionX;
        int newY = this.PositionY;

        Random random = new Random();

        direction = random.Next(0, 4);

        if (direction == (int)Direction.up)
        {
            newY--;
        }  
        else if (direction == (int)Direction.down)
        {
            newY++;
        }
        else if (direction == (int)Direction.left)
        {
            newX--;
        }
        else if (direction == (int)Direction.right)
        {
            newX++;
        }

        if (player.IsValidMove(newX, newY, _levelData))
        {
            this.PositionX = newX;
            this.PositionY = newY;
        }    
    }
}