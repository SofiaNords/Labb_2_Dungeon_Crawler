public class Player : LevelElement
{
    public virtual int HP { get; set; }

    public Dice AttackDice { get; set; }

    public Dice DefenceDice { get; set; }

    public Player(int x, int y, Dice attackDice, Dice defenceDice)
    {
        ClassChar = '@';
        Color = ConsoleColor.Magenta;
        PositionX = x;
        PositionY = y;
        HP = 100;

        AttackDice = attackDice;
        DefenceDice = defenceDice;
    }

}