// Klass som hanterar och visar spelets data, t.ex. spelarens status och aktuell spelomgång
public class GameData
{
    // En privat variabel som håller referens till nivådata
    private LevelData _levelData;

    // Konstruktor som tar emot nivådata och sparar det i den privata variabeln
    public GameData(LevelData levelData)
    {
        _levelData = levelData; // Spara referensen till nivådata
    }

    // Metod som visar spelarens information på skärmen, inklusive hälsa och aktuell spelomgång
    public void DisplayInfo(int turn)
    {
        // Hämta spelarens information från nivådata
        var player = _levelData.PlayerStartPosition;

        // Ställ in skrivpositionen i konsolen (i det här fallet, längst upp till vänster)
        Console.SetCursorPosition(0, 0);

        // Skriv ut spelarens namn, hälsa (nuvarande/total) och aktuellt antal omgångar (turn)
        Console.WriteLine($"Name: {player.Name} - Health: {player.HP}/100 - Turn: {turn}");
    }
}
