public class LevelData
{
    private List<LevelElement> _elements;

    public List<LevelElement> Elements
    {
        get { return _elements; }
    }

    public void Load(string filename)
    {
        string filePath = Path.Combine("Levels", filename);

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    Console.WriteLine(reader.ReadLine());
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