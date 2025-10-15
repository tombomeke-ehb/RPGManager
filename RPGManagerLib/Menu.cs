namespace RPGManagerLib
{
    public class Menu
    {
        public static void MakeCharacter()
        {
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();

            Console.WriteLine($"Welcome, {name}! Let's create your character.");
            Character newChar = null;
            bool validChoice = false;

            while (!validChoice)
            {
                Console.WriteLine("Choose your class:\n1: Warrior\n2: Mage");
                string typeOfClass = Console.ReadLine().ToLower();

                switch (typeOfClass)
                {
                    case "1":
                    case "warrior":
                        Console.WriteLine("You chose Warrior!");
                        Console.WriteLine("Now choose your weapons (type 'q' to finish):");

                        List<string> weapons = new List<string>();
                        while (true)
                        {
                            Console.Write("Add weapon: ");
                            string weapon = Console.ReadLine();
                            if (weapon.ToLower() == "q") break;
                            weapons.Add(weapon);
                        }

                        newChar = new Warrior(name, weapons);
                        validChoice = true;
                        break;

                    case "2":
                    case "mage":
                        Console.WriteLine("You chose Mage!");
                        newChar = new Mage(name);
                        validChoice = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please try again.\n");
                        break;
                }
            }

            Console.WriteLine("\nCharacter created successfully!");
            Console.WriteLine(newChar.ToString());

            Console.WriteLine($"\nYou are now entering the adventure as {newChar.Name}, the {newChar.CharacterType}...");
        }
    }
}
