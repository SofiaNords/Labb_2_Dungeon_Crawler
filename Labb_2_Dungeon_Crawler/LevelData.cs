public class LevelData
{
    // List to hold the elements that make up the level
    private List<LevelElement> _elements;

    // Exposing the elements list publicly, allowing read access while keeping the list itself private
    public List<LevelElement> Elements
    {
        get { return _elements; }
    }

    // Constructor to initialize the level elements list
    public LevelData()
    {
        _elements = new List<LevelElement>();
    }

    // Method to load level data from a specified file
    public void Load(string fileName)
    {
        // Constructing the file path to locate the level file
        string filePath = Path.Combine("Levels", fileName);

        try
        {
            // Using StreamReader to read the level file content
            using (StreamReader reader = new StreamReader(filePath))
            {
                int character;
                int positionX = 0; // Tracks the horizontal position in the level
                int positionY = 0; // Tracks the vertical position in the level

                // Reading characters from the file until the end is reached
                while ((character = reader.Read()) != -1)
                {
                    char c = (char)character; // Casting the character to char type

                    LevelElement element = null; // Variable to hold the level element being created

                    // Determining which type of level element to create based on the character read
                    switch (c)
                    {
                        case '#':
                            element = new Wall(positionX, positionY); // Create a wall element
                            break;
                        case 'r':
                            element = new Rat(positionX, positionY); // Create a rat element
                            break;
                        case 's':
                            element = new Snake(positionX, positionY); // Create a snake element
                            break;
                        case '@':
                            element = new Player(positionX, positionY); // Create a player element
                            break;
                        default:
                            element = null; // Ignore unrecognized characters
                            break;
                    };

                    // Adding the created element to the list if it is valid
                    if (element != null)
                    {
                        _elements.Add(element);
                    }

                    // Incrementing the x position for the next character
                    positionX++;

                    // If a new line character is encountered, reset x position and increment y position
                    if (c == '\n')
                    {
                        positionY++;
                        positionX = 0; // Resetting to the start of the next line
                    }
                }
            }
        }
        catch (IOException e)
        {
            // Handling potential file I/O errors
            Console.WriteLine("An error occurred");
            Console.WriteLine(e.Message);
        }
    }
}
