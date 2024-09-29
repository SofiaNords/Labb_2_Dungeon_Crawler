using System.Data;

public abstract class Enemy : LevelElement
{
    public virtual string Name { get; set; }

    public virtual int HP { get; set; }

    public int AttackDice { get; set; }

    public int DefenceDice { get; set; }

    public abstract void Update();
  
}