public class Dice
{
    private int _numberOfDice;
    private int _sidesPerDice;
    private int _modifier;

    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        this._numberOfDice = numberOfDice;
        this._sidesPerDice = sidesPerDice;
        this._modifier = modifier;
    }

    public int Throw()
    {
        Random random = new Random();
        int total = 0;

        for (int i = 0; i < _numberOfDice; i++)
        {
            total += random.Next(1, _sidesPerDice + 1);
        }

        total += _modifier;

        return total;
    }
}