public class Snake : Enemy
{
    public Snake(int x, int y)
    {
        ClassChar = 's';
        Color = ConsoleColor.Green;
        PositionX = x;
        PositionY = y;
        Name = "Snake";
        HP = 25;
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}