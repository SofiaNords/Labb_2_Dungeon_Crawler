using System.Security.Cryptography.X509Certificates;

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

        if (IsValidMove(newX, newY))
        {
            this.PositionX = newX;
            this.PositionY = newY;
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

    public int Defence() // denna behövs ej
    {
        int defenceScore = DefenceDice.Throw();

        return defenceScore;
    }

}