public class Rat : Enemy
{
    public Rat(int x, int y)
    {
        ClassChar = 'r';
        Color = ConsoleColor.Red;
        PositionX = x;
        PositionY = y;
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}