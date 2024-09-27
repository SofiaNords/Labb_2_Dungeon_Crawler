public class Wall: LevelElement 
{
    public Wall(int x, int y)
    {
        ClassChar = '#';
        Color = ConsoleColor.Gray;
        PositionX = x;
        PositionY = y;
    }
}
