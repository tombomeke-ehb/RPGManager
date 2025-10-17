using RPGManagerLib.Characters;
using RPGManagerLib.Saves;

namespace RPGManagerLib.UI
{
    public static class GameMenu
    {
        private static List<Character> characters = new();
        private static Character? currentCharacter = null;

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

            // Then show the main menu
            ShowMainMenu();
        }

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

        private static void Explore()
        {
            if (currentCharacter == null)
            {
                Console.WriteLine("Select a character first!");
                return;
            }

            Console.WriteLine($"{currentCharacter.Name} is exploring the world...");
        }

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
