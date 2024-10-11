// Klass som representerar en tärning eller flera tärningar med modifierare
public class Dice
{
    // Privat variabel som lagrar antal tärningar
    private int _numberOfDice;

    // Privat variabel som lagrar antalet sidor per tärning
    private int _sidesPerDice;

    // Privat variabel som lagrar modifieraren som ska läggas till resultatet
    private int _modifier;

    // En statisk Random-instans för att slumptala
    private static Random _random = new Random();

    // Konstruktor som tar emot antal tärningar, antal sidor per tärning och en modifierare
    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        this._numberOfDice = numberOfDice; // Sätt antal tärningar
        this._sidesPerDice = sidesPerDice; // Sätt antal sidor per tärning
        this._modifier = modifier; // Sätt modifieraren
    }

    // Metod som "slår" tärningarna och returnerar resultatet
    public int Throw()
    {
        int score = 0; // Variabel för att lagra det totala resultatet

        // Slå varje tärning och lägg till resultatet till score
        for (int i = 0; i < _numberOfDice; i++)
        {
            score += _random.Next(1, _sidesPerDice + 1); // Slå tärningen och addera till score
        }

        score += _modifier; // Lägg till modifieraren till det totala resultatet

        return score; // Returnera det totala resultatet
    }

    // Överskriven metod som returnerar en strängrepresentation av tärningen
    public override string ToString()
    {
        return $"{_numberOfDice}d{_sidesPerDice}+{_modifier}"; // Exempel: "2d6+3"
    }
}
