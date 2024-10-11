// Abstrakt bas-klass som representerar ett element på nivån (t.ex. fiende, spelare, föremål)
public abstract class LevelElement
{
    // Egenskaper för att hålla reda på elementets position på X- och Y-axeln
    public virtual int PositionX { get; set; }
    public virtual int PositionY { get; set; }

    // Egenskap som representerar elementets visuella symbol i spelet (t.ex. en karaktär som ritas ut)
    public char ClassChar { get; set; }

    // Egenskap för att definiera elementets färg när det ritas
    public ConsoleColor Color { get; set; }

    // Metod som ritar ut elementet på skärmen om det är inom spelarens synfält
    public void Draw(Player player)
    {
        // Kollar om elementet är inom spelarens synfält
        if (player.IsWithinVisionRange(this))
        {
            // Sätt färgen för att rita elementet på skärmen
            Console.ForegroundColor = Color;

            // Sätt konsolens markör till elementets position och rita ut symbolen (ClassChar)
            Console.SetCursorPosition(PositionX, 3 + PositionY); // Lägg till 3 för att ge lite utrymme vid Y-axeln
            Console.Write(ClassChar);

            // Återställ färgen till standard
            Console.ResetColor();
        }
    }
}
