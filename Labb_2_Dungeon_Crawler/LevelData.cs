public class LevelData
{
    private List<LevelElement> _elements;

    public List<LevelElement> Elements
    {
        get { return _elements; }
    }

    public LevelData()
    {
        _elements = new List<LevelElement>();
    }

    public void Load(string filename)
    {
        string filePath = Path.Combine("Levels", filename);

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                int character;
                while ((character = reader.Read()) != -1)
                {
                    char c = (char)character;

                    LevelElement element = c switch
                    {
                        '#' => new WallElement(),
                        'r' => new RatElement(),
                        's' => new SnakeElement(),
                        _ => null
                    };

                    if (element != null)
                    {
                        _elements.Add(element);
                    }
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Ett fel uppstod");
            Console.WriteLine(e.Message);
        }
    }
}