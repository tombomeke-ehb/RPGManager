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

                if (typeOfClass == "1" || typeOfClass == "warrior")
                {
                    Console.WriteLine("You chose Warrior!");
                    Console.WriteLine("Now choose your weapons (type 'q' to finish):");

                    List<string> weapons = new List<string>();
                    while (true)
                    {
                        Console.Write("Add weapon: ");
                        string weapon = Console.ReadLine();

                        if (weapon.ToLower() == "q")
                            break;

                        weapons.Add(weapon);
                    }

                    newChar = new Warrior(name, weapons);
                    validChoice = true;
                }
                else if (typeOfClass == "2" || typeOfClass == "mage")
                {
                    Console.WriteLine("You chose Mage!");
                    newChar = new Mage(name);
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice, please try again.\n");
                }
            }

            Console.WriteLine($"\nCharacter created successfully!");
            Console.WriteLine(newChar.ToString());

            Console.WriteLine("\nYou are now entering the adventure as " + newChar.Name + "...");
            // Later, you can call StartGame(newChar);
        }
    }
}
