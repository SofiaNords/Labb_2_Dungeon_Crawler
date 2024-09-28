LevelData levelData = new LevelData();

levelData.Load("Level1.txt");

foreach(LevelElement element in levelData.Elements)
{
    element.Draw();
}