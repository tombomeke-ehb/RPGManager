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

                        List<Weapons.Weapon> weapons = new List<Weapons.Weapon>();
                        int usedSlots = 0;
                        const int maxSlots = 4;

                        while (true)
                        {
                            Console.Write($"Add weapon (Sword, Bow, Staff or 'q' to finish) — {usedSlots}/{maxSlots} slots used: ");
                            string weaponInput = Console.ReadLine().ToLower();
                            if (weaponInput == "q") break;

                            Weapons.Weapon weapon = weaponInput switch
                            {
                                "sword" => new Weapons.Sword(),
                                "bow" => new Weapons.Bow(),
                                "staff" => new Weapons.Staff(),
                                _ => null
                            };

                            if (weapon == null)
                            {
                                Console.WriteLine("Invalid weapon type!");
                                continue;
                            }

                            int weaponSize = weapon.InventorySpaceAmount == Weapons.InventorySpaceAmount.SMALL ? 1 : 2;

                            if (usedSlots + weaponSize > maxSlots)
                            {
                                Console.WriteLine("You don't have enough space for that weapon!");
                                continue;
                            }

                            weapons.Add(weapon);
                            usedSlots += weaponSize;
                            Console.WriteLine($"{weapon.Name} added to inventory!");
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
