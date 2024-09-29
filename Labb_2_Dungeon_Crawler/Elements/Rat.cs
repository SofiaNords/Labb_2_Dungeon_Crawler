public class Rat : Enemy
{
    public Rat(int x, int y)
    {
        ClassChar = 'r';
        Color = ConsoleColor.Red;
        PositionX = x;
        PositionY = y;
        Name = "Rat";
        HP = 10;
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}