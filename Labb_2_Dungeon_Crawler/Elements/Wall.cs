// Klass som representerar en vägg på spelets nivå, som ärver från LevelElement
public class Wall : LevelElement
{
    // Konstruktor som initialiserar en vägg med en specifik position
    public Wall(int x, int y)
    {
        ClassChar = '#'; // Väggens visuella representation är '#'
        Color = ConsoleColor.Gray; // Färgen för väggen sätts till grå
        PositionX = x; // Sätt väggens X-position
        PositionY = y; // Sätt väggens Y-position
    }
}
