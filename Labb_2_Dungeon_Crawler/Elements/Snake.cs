public class Snake : Enemy
{
    private Player _player;
    public Snake(int x, int y, Player player)
    {
        ClassChar = 's';
        Color = ConsoleColor.Green;
        PositionX = x;
        PositionY = y;
        Name = "Snake";
        HP = 25;
        _player = player;
    }

    public override void Update()
    {
       
    }
}