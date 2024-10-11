using System;

public class GameLoop
{
    // För att hålla reda på spelnivåns data
    private LevelData _levelData;

    // Dictionary för att lagra tidigare positioner för alla nivåelement
    private Dictionary<LevelElement, (int X, int Y)> _previousPositions = new();

    // Objekt som hanterar attacklogik
    private Attack _attack;

    // Objekt som hanterar all speldata (t.ex. poäng, status)
    private GameData _gamedata;

    // Konstruktorn initialiserar nivådata, attack och speldatan
    public GameLoop(LevelData levelData)
    {
        _levelData = levelData;
        _attack = new Attack(_levelData); // Skapa en attackhanterare
        _gamedata = new GameData(_levelData); // Skapa ett objekt för speldata
    }

    // Huvudmetoden som kör spelets loop
    public void Run()
    {
        InitializeElementPositions(); // Initiera elementens positioner på nivån

        // Sätt spelarens startposition
        Player player = _levelData.PlayerStartPosition;
        int turn = 0;

        Console.CursorVisible = false; // Dölj muspekaren i konsolen

        while (true)
        {
            // Kolla om spelet är slut
            if (IsGameOver(player)) break;

            // Kontrollera om en tangenttryckning finns
            if (Console.KeyAvailable)
            {
                // Läs in tryckt tangent
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Beräkna spelarens nya position baserat på tryckt tangent
                var (newX, newY) = player.Move(keyInfo.Key);

                UpdateElementPositions(); // Uppdatera alla elementens positioner

                // Hitta fiende som är målet för attacken
                Enemy target = _attack.GetEnemyToAttack(newX, newY);

                if (target != null)
                {
                    // Utför attack på målet (fienden)
                    player.Attack(target);

                    // Om fienden överlever attacken, gör en motattack
                    if (target.HP > 0)
                    {
                        player.DefendFrom(target); // Fienden gör en motattack
                    }
                    else
                    {
                        // Visa att fienden dog på skärmen
                        Console.SetCursorPosition(0, 26);
                        Console.Write(new string(' ', Console.WindowWidth)); // Rensa tidigare meddelanden
                        Console.SetCursorPosition(0, 26);
                        Console.WriteLine($"{target.Name} died");
                    }
                }

                // Om den nya positionen är giltig, uppdatera spelarens position
                if (player.IsValidMove(newX, newY, _levelData))
                {
                    player.PositionX = newX;
                    player.PositionY = newY;
                }

                // Uppdatera varje fiendes logik baserat på spelarens position
                foreach (var element in _levelData.Elements)
                {
                    if (element is Enemy enemy)
                    {
                        enemy.Update(player);
                    }
                }

                // Ta bort döda fiender från listan
                RemoveDeadEnemies();

                turn++; // Öka turen
                DrawLevel(player); // Rita om spelets nivå
                _gamedata.DisplayInfo(turn); // Visa spelinformation (t.ex. tur, poäng)
            }
        }
    }

    // Kontrollera om spelet är slut (om spelaren är död)
    private bool IsGameOver(Player player)
    {
        if (player.HP <= 0)
        {
            Console.Clear(); // Rensa konsolen
            Console.WriteLine("GAME OVER!"); // Visa "GAME OVER"-meddelande
            return true; // Spelet är slut
        }
        return false; // Spelet fortsätter
    }

    // Ta bort döda fiender från nivån
    private void RemoveDeadEnemies()
    {
        // Hitta alla döda fiender
        var deadEnemies = _levelData.Elements.OfType<Enemy>()
                                           .Where(e => e.HP <= 0)
                                           .ToList();

        // Ta bort varje död fiende från listan
        foreach (var deadEnemy in deadEnemies)
        {
            _levelData.Elements.Remove(deadEnemy);
        }
    }

    // Initiera positionerna för alla nivåelement
    private void InitializeElementPositions()
    {
        foreach (var element in _levelData.Elements)
        {
            _previousPositions[element] = (element.PositionX, 3 + element.PositionY); // Lägg till elementets initiala position i dictionaryn
        }
    }

    // Uppdatera positionerna för alla nivåelement
    private void UpdateElementPositions()
    {
        foreach (var element in _levelData.Elements)
        {
            var previousPos = _previousPositions[element];

            // Om elementet har flyttat, uppdatera dess position
            if (element.PositionX != previousPos.Item1 || element.PositionY != previousPos.Item2)
            {
                _previousPositions[element] = (element.PositionX, 3 + element.PositionY); // Spara den nya positionen
            }
        }
    }

    // Rita om hela spelets nivå
    private void DrawLevel(Player player)
    {
        foreach (LevelElement element in _levelData.Elements)
        {
            // Kontrollera om objektet är inom spelarens synfält innan det ritas
            if (player.IsWithinVisionRange(element) && element != player)
            {
                var previousPos = _previousPositions[element];
                if (element.PositionX != previousPos.Item1 || element.PositionY != previousPos.Item2)
                {
                    // Rensa den gamla platsen och rita det nya elementet
                    Console.SetCursorPosition(previousPos.Item1, previousPos.Item2);
                    Console.Write(" "); // Rensa den gamla platsen

                    Console.SetCursorPosition(element.PositionX, element.PositionY);
                    element.Draw(player); // Rita elementet på den nya positionen
                }
            }
        }

        // Rita om spelarens position
        var previousPlayerPos = _previousPositions[player];
        Console.SetCursorPosition(previousPlayerPos.Item1, previousPlayerPos.Item2);
        Console.Write(" "); // Rensa den gamla platsen

        Console.SetCursorPosition(player.PositionX, player.PositionY);
        player.Draw(player); // Rita spelarens nya position
    }
}
