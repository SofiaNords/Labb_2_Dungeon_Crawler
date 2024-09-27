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
                int positionX = 0;
                int positionY = 0;

                while ((character = reader.Read()) != -1)
                {
                    char c = (char)character;

                    LevelElement element = null;

                    switch (c)
                    {
                        case '#':
                            element = new Wall(positionX, positionY);
                            break;
                        case 'r':
                            element = new Rat(positionX, positionY);
                            break;
                        case 's':
                            element = new Snake(positionX, positionY);
                            break;
                        default:
                            element = null;
                            break;
                    };

                    if (element != null)
                    {
                        _elements.Add(element); 
                    }

                    positionX++;

                    if (c == '\n')
                    {
                        positionY++;
                        positionX = 0;
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