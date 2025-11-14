using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Saves;
using RPGManagerLib.Worlds;

namespace RPGManagerLib.UI
{
    /// <summary>
    /// High-level console game loop and character management menu.
    /// </summary>
    public static class GameMenu
    {
        private static List<Character> characters = new();
        private static Character? currentCharacter = null;

        /// <summary>
        /// Initializes state, loads characters, and enters the main menu.
        /// </summary>
        public static void Start()
        {
            // Load saved characters
            characters = SaveManager.LoadCharacters();

            // If no characters exist, force creation
            if (characters.Count == 0)
            {
                Console.WriteLine("Welcome, brave adventurer! Let's create your first hero.");
                var firstChar = CharacterFactory.CreateCharacter();
                characters.Add(firstChar);
                currentCharacter = firstChar;
                SaveManager.SaveCharacters(characters);

                Console.WriteLine($"\nCharacter '{firstChar.Name}' created! Your adventure begins...\n");
            }
            else
            {
                // Load last played character
                currentCharacter = characters[0]; // or track last played
            }

            // Generate world
            
            World Tavaryn = new("Tavaryn", "A quite small starter world with not that many dangerous monsters");
            Tavaryn.UnLock();
            currentCharacter.CurrentWorld = Tavaryn;

            // Then show the main menu
            ShowMainMenu();
            
        }

        /// <summary>
        /// Displays the main menu and handles user interactions for managing characters and gameplay options.
        /// </summary>
        /// <remarks>The main menu provides options to create a new character, switch between existing
        /// characters,  view the current character, explore, engage in combat, or quit the application. Each option 
        /// performs the corresponding action and may involve saving character data or updating the current  character
        /// state. The menu runs in a loop until the user chooses to quit.</remarks>
        private static void ShowMainMenu()
        {
            var mainMenu = new MenuSystem("MAIN MENU");

            mainMenu.AddOption("1", "New Character", () =>
            {
                var newChar = CharacterFactory.CreateCharacter();
                characters.Add(newChar);
                currentCharacter = newChar;
                SaveManager.SaveCharacters(characters);
            });

            mainMenu.AddOption("2", "Switch Character", () =>
            {
                SwitchCharacter();
                SaveManager.SaveCharacters(characters);
            });

            mainMenu.AddOption("3", "View Current Character", () =>
            {
                if (currentCharacter != null)
                    Console.WriteLine(currentCharacter);
                else
                    Console.WriteLine("No character selected.");
            });

            mainMenu.AddOption("4", "Explore", () => Explore());
            mainMenu.AddOption("5", "Fight", () => Fight());
            mainMenu.AddOption("6", "Quit", () =>
            {
                SaveManager.SaveCharacters(characters);
                Environment.Exit(0);
            });

            while (true)
            {
                mainMenu.Show();
                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Allows the user to switch the currently active character by selecting from a list of available characters.
        /// </summary>
        /// <remarks>If no characters are available, the method displays a message and exits without
        /// making any changes. The user is prompted to select a character by entering the corresponding number from the
        /// displayed list. If the input is invalid or out of range, an error message is displayed, and the active
        /// character remains unchanged.</remarks>
        private static void SwitchCharacter()
        {
            if (characters.Count == 0)
            {
                Console.WriteLine("No characters available! Create one first.");
                return;
            }

            Console.WriteLine("\nAvailable Characters:");
            for (int i = 0; i < characters.Count; i++)
                Console.WriteLine($"{i + 1}. {characters[i].Name} ({characters[i].CharacterType})");

            Console.Write("Enter number to select: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice > 0 && choice <= characters.Count)
            {
                currentCharacter = characters[choice - 1];
                Console.WriteLine($"You are now playing as {currentCharacter.Name}!");
            }
            else
            {
                Console.WriteLine("Invalid selection!");
            }
        }

        /// <summary>
        /// Initiates an exploration action for the currently selected character.
        /// </summary>
        /// <remarks>This method outputs a message indicating the exploration activity of the selected
        /// character. If no character is selected, a message prompting the user to select a character is
        /// displayed.</remarks>
        private static void Explore()
        {
            if (currentCharacter == null)
            {
                Console.WriteLine("Select a character first!");
                return;
            }

            Console.WriteLine($"{currentCharacter.Name} is exploring the world...");
        }

        /// <summary>
        /// Initiates a battle sequence for the currently selected character.
        /// </summary>
        /// <remarks>This method requires a character to be selected before it is called.  If no character
        /// is selected, a message will be displayed, and the method will exit without performing any action.</remarks>
        private static void Fight()
        {
            if (currentCharacter == null)
            {
                Console.WriteLine("Select a character first!");
                return;
            }

            Console.WriteLine($"{currentCharacter.Name} is preparing for battle!");
        }

    }

}
