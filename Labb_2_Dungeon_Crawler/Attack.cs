// Klass som hanterar attacker i spelet
public class Attack
{
    // En privat variabel som håller referensen till nivådata
    private LevelData _levelData;

    // Konstruktor som tar emot nivådata och sparar det i den privata variabeln
    public Attack(LevelData levelData)
    {
        _levelData = levelData; // Spara referensen till nivådata
    }

    // Metod som kontrollerar om det finns en fiende på den angivna positionen och returnerar den fienden
    public Enemy GetEnemyToAttack(int x, int y)
    {
        // Loopar genom alla element på nivån för att kolla om en fiende finns på den nya positionen
        foreach (var element in _levelData.Elements)
        {
            // Om ett element finns på samma position och är en fiende, returnera detta element som en Enemy
            if (element.PositionX == x && element.PositionY == y && (element is Enemy))
            {
                return (Enemy)element; // Returnera fienden som ett Enemy-objekt
            }
        }
        // Om ingen fiende finns på den angivna positionen, returnera null
        return null;
    }
}
