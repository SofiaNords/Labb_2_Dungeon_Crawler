using System.Data;

public abstract class Enemy : LevelElement
{
    public virtual string Name { get; set; }

    public virtual int HP { get; set; }

    public Dice AttackDice { get; set; }

    public Dice DefenceDice { get; set; }

    public abstract void Update();
  
}