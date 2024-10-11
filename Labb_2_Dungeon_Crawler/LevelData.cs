using System.Security.Principal;

public class LevelData
{
    // Lista för att lagra alla element som utgör nivåns objekt
    private List<LevelElement> _elements;

    // Egenskap för att hämta spelarens startposition på nivån
    public Player PlayerStartPosition { get; private set; }

    // Egenskap för att hålla referens till en fiende, kan vara en enda fiende för nu
    public Enemy Enemy { get; set; }

    // Offentlig egenskap för att få tillgång till listan av element, men hålla själva listan privat
    public List<LevelElement> Elements
    {
        get { return _elements; }
    }

    // Konstruktor för att initialisera listan av nivåelement
    public LevelData()
    {
        _elements = new List<LevelElement>(); // Skapa en ny lista för element på nivån
    }

    // Metod för att läsa in nivådata från en specifik fil
    public void Load(string fileName)
    {
        // Skapa sökvägen till nivåfilen
        string filePath = Path.Combine("Levels", fileName);

        try
        {
            // Använd StreamReader för att läsa innehållet i nivåfilen
            using (StreamReader reader = new StreamReader(filePath))
            {
                int character;
                int positionX = 0; // Håller koll på den horisontella positionen i nivån
                int positionY = 0; // Håller koll på den vertikala positionen i nivån

                // Läs varje tecken i filen tills slutet av filen
                while ((character = reader.Read()) != -1)
                {
                    char c = (char)character; // Konvertera det lästa tecknet till en char

                    LevelElement element = null; // Variabel för att hålla det element som skapas

                    // Bestäm vilken typ av element som ska skapas baserat på det lästa tecknet
                    switch (c)
                    {
                        case '#': // Väggelement
                            element = new Wall(positionX, positionY);
                            break;
                        case 'r': // Rattelement
                            element = new Rat(positionX, positionY, this);
                            break;
                        case 's': // Ormelement
                            element = new Snake(positionX, positionY, this);
                            break;
                        case '@': // Spelarelement
                            element = new Player(positionX, positionY);
                            PlayerStartPosition = (Player)element; // Sätt spelarens startposition
                            break;
                        default:
                            element = null; // Ignorera okända tecken
                            break;
                    }

                    // Om ett giltigt element har skapats, lägg till det i listan
                    if (element != null)
                    {
                        _elements.Add(element);
                    }

                    // Öka x-positionen för nästa tecken
                    positionX++;

                    // Om ett radbrytningstecken ('\n') läses, öka y-positionen och återställ x-positionen
                    if (c == '\n')
                    {
                        positionY++;
                        positionX = 0; // Återställ till startpositionen för nästa rad
                    }
                }
            }
        }
        catch (IOException e)
        {
            // Hantera potentiella fel vid filinläsning
            Console.WriteLine("Ett fel uppstod när nivån skulle läsas in:");
            Console.WriteLine(e.Message);
        }
    }
}
