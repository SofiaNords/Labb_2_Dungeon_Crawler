public class Player : LevelElement
{
    public int HP { get; set; }

    public Dice AttackDice { get; set; }

    public Dice DefenceDice { get; set; }

    public Player(int x, int y)
    {
        ClassChar = '@';
        Color = ConsoleColor.Magenta;
        PositionX = x;
        PositionY = y;
        HP = 100;
        AttackDice = new Dice(2, 6, 2);
        DefenceDice = new Dice(2, 6, 0); 
    }

    //public int AttackEnemy()
    //{
    //    int attackScore = AttackDice.Throw();

    //    return attackScore;
    //}


}