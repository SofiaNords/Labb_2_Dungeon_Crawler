// Abstrakt klass som representerar en fiende i spelet, som är en typ av LevelElement
public abstract class Enemy : LevelElement
{
    // Egenskap för att hålla fiendens namn (t.ex. "Rat", "Snake")
    public virtual string Name { get; set; }

    // Egenskap för fiendens hälsopoäng (HP)
    public virtual int HP { get; set; }

    // Dice (Tärning) som används för att beräkna attackskada
    public Dice AttackDice { get; set; }

    // Dice (Tärning) som används för att beräkna försvar (defense)
    public Dice DefenceDice { get; set; }

    // Abstrakt metod som måste implementeras av alla fiendetyper för att uppdatera fiendens beteende
    public abstract void Update(Player player);
}
