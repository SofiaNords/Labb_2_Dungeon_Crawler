using System.Numerics;

public class Player : LevelElement
{
    // Spelarens namn
    public string Name { get; set; }

    // Spelarens hälsa
    public int HP { get; set; }

    // Spelarens attacktärning
    public Dice AttackDice { get; set; }

    // Spelarens försvarstärning
    public Dice DefenceDice { get; set; }

    // Spelarens synradie
    public int VisionRange { get; set; }

    // Konstruktor för att skapa en spelare med startposition och attribut
    public Player(int x, int y)
    {
        ClassChar = '@';  // Spelarens representation i spelet (symbol)
        Color = ConsoleColor.Yellow;  // Spelarens färg i spelet
        PositionX = x;  // Spelarens startposition (X)
        PositionY = y;  // Spelarens startposition (Y)
        Name = "Sofia";  // Spelarens namn
        HP = 100;  // Spelarens hälsa
        AttackDice = new Dice(2, 6, 2);  // Tärning för attack (2 tärningar med 6 sidor och modifiering 2)
        DefenceDice = new Dice(2, 6, 0);  // Tärning för försvar (2 tärningar med 6 sidor och utan modifiering)
        VisionRange = 6;  // Spelarens synradie (hur många rutor hen kan se)
    }

    // Metod för att flytta spelaren baserat på användarens tangenttryckning
    public (int newX, int newY) Move(ConsoleKey key)
    {
        int newX = PositionX;
        int newY = PositionY;

        // Kolla på vilken tangent som trycktes och uppdatera spelarens position
        switch (key)
        {
            case ConsoleKey.UpArrow:  // Uppåt
                newY--;
                break;
            case ConsoleKey.DownArrow:  // Nedåt
                newY++;
                break;
            case ConsoleKey.LeftArrow:  // Vänster
                newX--;
                break;
            case ConsoleKey.RightArrow:  // Höger
                newX++;
                break;
        }

        return (newX, newY);  // Returnera den nya positionen som en tuple (newX, newY)
    }

    // Metod som kollar om ett objekt är inom spelarens synradie
    public bool IsWithinVisionRange(LevelElement element)
    {
        // Beräkna avståndet till objektet och kolla om det är inom synradien
        int distance = CalculateDistance(this.PositionX, this.PositionY, element.PositionX, element.PositionY);
        return distance <= VisionRange;
    }

    // Beräknar avståndet mellan spelaren och ett annat objekt (Pythagoras sats för Manhattan-avstånd)
    private int CalculateDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);  // Manhattan-avstånd (summa av de absoluta skillnaderna i x- och y-koordinater)
    }

    // Metod för att attackera en fiende
    public void Attack(Enemy enemy)
    {
        // Rulla attacktärning och försvarstärning för att beräkna skada
        int attackScore = AttackDice.Throw();
        int defenceScore = enemy.DefenceDice.Throw();
        int damage = Math.Max(0, attackScore - defenceScore);  // Skadan kan inte bli negativ

        Console.ForegroundColor = ConsoleColor.Yellow;

        if (damage > 0)
        {
            enemy.HP -= damage;  // Minska fiendens hälsa med skadan
        }

        // Skriv ut information om attacken i konsolen
        Console.SetCursorPosition(0, 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 1);
        Console.WriteLine($"You (ATK: {AttackDice} => {attackScore}) attacked the {enemy.Name} (DEF: {enemy.DefenceDice} => {defenceScore})");

        // Skriv ut fiendens återstående hälsa
        Console.SetCursorPosition(0, 25);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 25);
        Console.WriteLine($"{enemy.Name} HP is {enemy.HP:D2}");

        Console.ResetColor();
    }

    // Metod för att försvara sig mot en fiendes attack
    public void DefendFrom(Enemy enemy)
    {
        int attackScore = AttackDice.Throw();
        int defenceScore = enemy.DefenceDice.Throw();
        int damage = Math.Max(0, attackScore - defenceScore);  // Beräkna skadan som tas

        Console.ForegroundColor = ConsoleColor.Red;

        if (damage > 0)
        {
            this.HP -= damage;  // Minska spelarens hälsa
            Console.SetCursorPosition(0, 2);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"The {enemy.Name} (ATK: {AttackDice} => {attackScore}) attacked you (DEF: {enemy.DefenceDice} => {defenceScore})");
        }

        if (this.HP <= 0)
        {
            // Om spelarens hälsa är 0 eller lägre, avsluta spelet
            Console.Clear();
            Console.WriteLine($"You (the player) die! Game over.");
        }

        Console.ResetColor();
    }

    // Metod för att kontrollera om en rörelse är giltig (inte kolliderar med väggar eller fiender)
    public bool IsValidMove(int x, int y, LevelData levelData)
    {
        foreach (var element in levelData.Elements)
        {
            // Om det finns en vägg eller fiende på den nya positionen, är rörelsen ogiltig
            if (element.PositionX == x && element.PositionY == y && (element is Wall || element is Enemy))
            {
                return false;
            }
        }
        return true;  // Om ingen kollision finns, är rörelsen giltig
    }
}
