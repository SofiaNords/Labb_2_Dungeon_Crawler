// Skapa en instans av LevelData, som håller information om nivån (t.ex. spelobjekt, fiender, banor, etc.)
LevelData levelData = new LevelData();

// Ladda data från en fil (t.ex. "Level1.txt") som innehåller information om nivån. Denna metod hämtar all relevant nivådata.
levelData.Load("Level1.txt");

// Skapa en instans av GameLoop, som ansvarar för att köra spelets huvudloop (uppdatera spelets tillstånd, hantera användarinmatning, etc.).
GameLoop gameLoop = new GameLoop(levelData);


// Loopa igenom alla element i levelData (t.ex. objekt, fiender, hinder, etc.) och rita varje element på skärmen.
foreach (LevelElement element in levelData.Elements)
{
    // Rita varje nivåelement (exempelvis objekt som fiender eller hinder) på skärmen.
    element.Draw(levelData.PlayerStartPosition);
}

// Starta spelets huvudloop. Detta kommer att kontinuerligt uppdatera spelets tillstånd och rendera det på skärmen.
gameLoop.Run();
