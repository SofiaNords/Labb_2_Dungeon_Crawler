public class Attack
{
    private LevelData _levelData;

    public Attack(LevelData levelData)
    {
        _levelData = levelData;
    }

    public bool IsAttackMove(int x, int y)
    {
        foreach (var element in _levelData.Elements)
        {
            if (element.PositionX == x && element.PositionY == y && (element is Enemy))
            {
                return true;
            }
        }
        return false;
    }
}