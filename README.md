# Lab 2 - Dungeon Crawler

This is my second assignment in the course Programming with C# in the .NET Developer program at IT-högskolan in Gothenburg. The task was to create a console application game called Dungeon Crawler. A dungeon crawler is a type of role-playing game where players explore labyrinthine areas, known as dungeons, where they fight enemies and search for treasures. In this lab, we will build a somewhat simplified version of such a game in the form of a console application.

The roguelike genre often relies on procedural generation, which is a method for generating random levels using algorithms. Since the focus of this lab is object-oriented programming, I have decided to omit this part and instead provide you with a pre-made level in the form of a text file. (Download Level1.txt, at the bottom of the page.)

The file represents a "dungeon" with two types of monsters ("rats" and "snakes") placed within it, and it also includes a predefined starting position for the player. Your task will be to write code that reads the file and divides it into different objects (walls, player, and enemies) in C#, where each object independently tracks its own data (e.g., position, color, health) and methods (e.g., for moving or attacking).
   

## The Task

### Lab 2 - Dungeon Crawler

#### Class Hierarchy of Level Elements

In addition to the player, there are 3 different types of objects in our "dungeon": "Wall", "Rat", and "Snake". We want to use inheritance to reuse as much code as possible for functionality that is shared between multiple types of objects.

There should be an abstract base class, which I’ve chosen to call "LevelElement". Since it is abstract, you cannot create instances of it; instead, it is used to define base functionality that other classes can inherit. LevelElement should have properties for (X,Y) position, a char that stores which character a class is drawn with (e.g., "Wall" will use the # symbol), and a ConsoleColor to store the color the character should be drawn in. It should also have a public Draw method (without parameters), which we can call to render a LevelElement with the correct color and character at the correct location.

The Wall class inherits from LevelElement, and really doesn’t need any additional code, except for hardcoding the color and symbol for a wall (a gray hashtag).

The Enemy class also inherits from LevelElement, and adds functionality specific to enemies. Enemy is abstract as well, because we don’t want to instantiate generic “enemies”. All real enemies (in the lab, Rat and Snake, but you can add more types if you want and have time) inherit from this class. Enemy should have properties for name (e.g., snake/rat), health (HP), and AttackDice and DefenceDice, which are of type Dice (more on this below). It should also have an abstract Update method, which is not implemented in this class, but requires that all subclasses implement it. We want to be able to call the Update method on all enemies, and then each subclass will handle how they update (for example, different movement patterns).

Finally, we have the Rat and Snake classes, which initialize their inherited properties with the unique attributes of each enemy and also implement the Update method in their own unique ways.

#### Loading Level Design from File

Create a class called LevelData that has a private field elements of type List<LevelElement>. This field should also be exposed via a public readonly property called Elements.

Additionally, LevelData should have a method called Load(string filename), which loads data from the file provided as an argument. The Load method reads through the text file character by character, and for each character it finds that is either #, r, or s, it creates a new instance of the class corresponding to that character and adds a reference to that instance to the elements list.

Keep in mind that when the instance is created, it must also receive an (X/Y) position, meaning that Load needs to keep track of the current row and column in the file where the character was found. It should also store the player's starting position when it encounters @.

Once the file is loaded, there should be an object in elements for every character in the file (excluding spaces and line breaks), and a simple foreach loop that calls .Draw() on each element in the list should now render the entire level on the screen.

#### Game Loop

A game loop is a loop that runs repeatedly while the game is active, and in our case, each iteration of the loop will correspond to one round of the game. For each iteration of the loop, we will wait for the user to press a key; then, we perform the player's move, followed by the computer's move (updating all enemies), before looping again. Optionally, we could have a key (such as Esc) to exit the loop/terminate the game.

When the player or enemies move, we need to calculate their new position and then check through all LevelElements to see if there is any other object at the location they are trying to move to. If there is a wall or another object (enemy/player) at that location, the movement must be canceled, and the previous position remains. However, note that if the player moves to a location where there is an enemy, they will attack that enemy (more on this later). The same applies if an enemy moves to the player's location. Enemies cannot, however, attack each other in the game.

#### Vision range

To create a sense of exploration in the game, we limit the player's field of view to only show objects within a radius of 5 characters (but you can also experiment with different radii). However, walls will never disappear once they have been seen, but enemies will not be visible once they move outside of the radius.

The distance between two points in 2D can be easily calculated using the Pythagorean theorem.

#### Rolling Dice

The game uses simulated dice rolls to determine how much damage the player and enemies inflict on each other.

Create a class called Dice with a constructor that takes 3 parameters: numberOfDice, sidesPerDice, and Modifier. By creating new instances of this class, you will be able to create different sets of dice, e.g., “3d6+2”, meaning rolling 3 six-sided dice, adding the results together, and then adding 2 to get the total score.

Dice objects should have a public Throw() method that returns an integer representing the score obtained when rolling the dice according to the object’s configuration. Each call to Throw() should represent a new roll of the dice.

Also, override the Dice.ToString() method so that when you print a Dice object, it returns a string that describes the object’s configuration, e.g., “3d6+2”.

#### Attack and Defense

When a player attacks (or enters into combat with) an enemy, or vice versa, we need to simulate dice rolls to get a score that determines how much damage the attack inflicts. The attacker rolls their dice first and gets an attack score. Then, the defender rolls their dice and gets a defense score. Subtract the defense score from the attack score, and if the difference is greater than 0, subtract that amount from the defender’s HP (health points). After one or more attacks, the HP reaches 0, causing the enemy to die (or the player to get a game over).

If the defender survives, they immediately perform a counterattack, meaning they roll dice again to get an attack score, and the attacker now defends by rolling their dice. Subtract HP according to the rules above.

The player and all types of enemies have a set of dice configurations for their attack and defense, as well as a hardcoded HP to start with. I've used the following configurations, but feel free to try others:

Player: HP = 100, Attack = 2d6+2, Defence = 2d6+0
Rat: HP = 10, Attack = 1d6+3, Defence = 1d6+1
Snake: HP = 25, Attack = 3d4+2, Defence = 1d8+5


#### Movement Patterns

The player moves 1 step up, down, left, or right each round, or stays still, depending on which key the user presses.

The Rat moves 1 step in a randomly chosen direction (up, down, left, or right) each round.

The Snake stays still if the player is more than 2 spaces away; otherwise, it moves away from the player.

## My Solution

My solution reflects the task in many ways with a few exceptions.

### Exceptions

### Draw method with parameter

*"It should also have a public Draw method (without parameters), which we can call to render a LevelElement with the correct color and character at the correct location."*

In my Vision Range solution I chose to place the methods for checking if an object is within the player's line of sight, as well as the method for calculating the distance to another object, in my Player class. In order to perform the check for whether an element is within the player's field of view, I pass the player as an argument to the Draw method in the LevelElement class.

