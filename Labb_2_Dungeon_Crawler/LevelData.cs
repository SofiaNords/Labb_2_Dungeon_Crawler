public class LevelData
{
    // List to store the elements that make up the level
    private List<LevelElement> _elements;

    // Property to get the starting player of the level
    public Player StartPlayer { get; private set; }

    // Public access to the elements list, allowing read-only access while keeping the list itself private
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
        // Construct the file path to locate the level file
        string filePath = Path.Combine("Levels", fileName);

        try
        {
            // Use StreamReader to read the level file content
            using (StreamReader reader = new StreamReader(filePath))
            {
                int character;
                int positionX = 0; // Tracks the horizontal position in the level
                int positionY = 0; // Tracks the vertical position in the level

                // Read characters from the file until the end is reached
                while ((character = reader.Read()) != -1)
                {
                    char c = (char)character; // Cast the integer character to char type

                    LevelElement element = null; // Variable to hold the level element being created

                    // Determine the type of level element to create based on the character read
                    switch (c)
                    {
                        case '#':
                            element = new Wall(positionX, positionY); // Create a wall element
                            break;
                        case 'r':
                            element = new Rat(positionX, positionY, this); // Create a rat element
                            break;
                        case 's':
                            element = new Snake(positionX, positionY); // Create a snake element
                            break;
                        case '@':
                            element = new Player(positionX, positionY); // Create a player element
                            StartPlayer = (Player)element; // Set the starting player
                            break;
                        default:
                            element = null; // Ignore unrecognized characters
                            break;
                    }

                    // Add the created element to the list if it is valid
                    if (element != null)
                    {
                        _elements.Add(element);
                    }

                    // Increment the x position for the next character
                    positionX++;

                    // If a newline character is encountered, reset x position and increment y position
                    if (c == '\n')
                    {
                        positionY++;
                        positionX = 0; // Reset to the start of the next line
                    }
                }
            }
        }
        catch (IOException e)
        {
            // Handle potential file I/O errors
            Console.WriteLine("An error occurred while loading the level:");
            Console.WriteLine(e.Message);
        }
    }
}
