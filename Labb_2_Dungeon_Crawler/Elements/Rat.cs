// Klass som representerar en specifik fiende, Råtta, som ärver från Enemy
public class Rat : Enemy
{
    // En privat variabel för att hålla referens till nivådata
    private LevelData _levelData;

    // Konstruktor som initialiserar en råtta med specifik position, nivådata och statistik
    public Rat(int x, int y, LevelData levelData)
    {
        ClassChar = 'r'; // Råttan representeras av bokstaven 'r'
        Color = ConsoleColor.Red; // Färgen för råttan sätts till röd
        PositionX = x; // Sätt råttans X-position
        PositionY = y; // Sätt råttans Y-position
        Name = "Rat"; // Namnet på fienden sätts till "Rat"
        HP = 10; // Råttan börjar med 10 hälsopoäng
        _levelData = levelData; // Spara referensen till nivådata
        AttackDice = new Dice(1, 6, 3); // Råttans attacktärning (1-6 sidor, 3 som modifierare)
        DefenceDice = new Dice(1, 6, 1); // Råttans försvarstärning (1-6 sidor, 1 som modifierare)
    }

    // En enum som definierar de fyra möjliga rörelseriktningarna för råttan
    public enum Direction
    {
        up,    // Uppåt
        down,  // Nedåt
        left,  // Vänster
        right, // Höger
    }

    // Metod som uppdaterar fiendens beteende. I detta fall flyttar råttan slumpmässigt.
    public override void Update(Player player)
    {
        int direction; // Variabel för att hålla riktningen råttan ska röra sig i
        int newX = this.PositionX; // Ny X-position för råttan
        int newY = this.PositionY; // Ny Y-position för råttan

        Random random = new Random(); // Skapa en slumpgenerator

        // Slumpa en riktning mellan 0 och 3
        direction = random.Next(0, 4);

        // Bestäm ny position beroende på slumpad riktning
        if (direction == (int)Direction.up)
        {
            newY--; // Flytta råttan uppåt
        }
        else if (direction == (int)Direction.down)
        {
            newY++; // Flytta råttan nedåt
        }
        else if (direction == (int)Direction.left)
        {
            newX--; // Flytta råttan åt vänster
        }
        else if (direction == (int)Direction.right)
        {
            newX++; // Flytta råttan åt höger
        }

        // Om den nya positionen är en giltig rörelse, uppdatera råttans position
        if (player.IsValidMove(newX, newY, _levelData))
        {
            this.PositionX = newX; // Uppdatera råttans X-position
            this.PositionY = newY; // Uppdatera råttans Y-position
        }
    }
}
