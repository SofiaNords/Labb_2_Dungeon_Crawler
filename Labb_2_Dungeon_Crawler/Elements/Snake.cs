// Klass som representerar en specifik fiende, Orm, som ärver från Enemy
public class Snake : Enemy
{
    // En privat variabel för att hålla referens till nivådata
    private LevelData _levelData;

    // Konstruktor som initialiserar en orm med specifik position, nivådata och statistik
    public Snake(int x, int y, LevelData levelData)
    {
        ClassChar = 's'; // Ormen representeras av bokstaven 's'
        Color = ConsoleColor.Green; // Färgen för ormen sätts till grön
        PositionX = x; // Sätt ormens X-position
        PositionY = y; // Sätt ormens Y-position
        Name = "Snake"; // Namnet på fienden sätts till "Snake"
        HP = 25; // Ormen börjar med 25 hälsopoäng
        _levelData = levelData; // Spara referensen till nivådata
        AttackDice = new Dice(3, 4, 2); // Ormens attacktärning (3-4 sidor, 2 som modifierare)
        DefenceDice = new Dice(1, 8, 5); // Ormens försvarstärning (1-8 sidor, 5 som modifierare)
    }

    // Metod som uppdaterar ormens beteende, t.ex. att röra sig mot spelaren
    public override void Update(Player player)
    {
        // Hämta spelarens position
        int playerPositionX = player.PositionX;
        int playerPositionY = player.PositionY;

        // Beräkna avståndet mellan ormen och spelaren i X- och Y-led
        int distanceX = Math.Abs(PositionX - playerPositionX);
        int distanceY = Math.Abs(PositionY - playerPositionY);

        // Variabler för att hålla ormens nya position
        int newX = PositionX;
        int newY = PositionY;

        // Om avståndet mellan ormen och spelaren är mindre än eller lika med 2, börja röra sig mot spelaren
        if (distanceX <= 2 && distanceY <= 2)
        {
            // Rörelse i X-led (om ormen är till vänster om spelaren, rör den sig åt vänster, annars åt höger)
            if (PositionX < playerPositionX)
            {
                newX--; // Rör sig åt höger
            }
            else
            {
                newX++; // Rör sig åt vänster
            }

            // Rörelse i Y-led (om ormen är ovanför spelaren, rör den sig uppåt, annars nedåt)
            if (PositionY < playerPositionY)
            {
                newY--; // Rör sig uppåt
            }
            else
            {
                newY++; // Rör sig nedåt
            }
        }

        // Om den nya positionen är giltig, uppdatera ormens position
        if (player.IsValidMove(newX, newY, _levelData))
        {
            PositionX = newX; // Uppdatera ormens X-position
            PositionY = newY; // Uppdatera ormens Y-position
        }
    }
}
