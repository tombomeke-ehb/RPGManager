using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Exceptions;
using RPGManagerLib.Characters.Heroes;

namespace RPGManagerLib.UI
{
    /// <summary>
    /// Provides functionality to create character instances based on user input.
    /// </summary>
    /// <remarks>This factory class interacts with the user via the console to gather input for character
    /// creation. It allows the user to specify a name and select a character class (e.g., Warrior or Mage).  For
    /// Warriors, the user can also select equipment through an interactive menu.</remarks>
    public static class CharacterFactory
    {
        /// <summary>
        /// Creates a new character based on user input.
        /// </summary>
        /// <remarks>This method prompts the user to enter a name and select a class for the character. 
        /// The available classes are Warrior and Mage. If the input does not match a valid class,  a Warrior with
        /// default equipment is created as a fallback.</remarks>
        /// <returns>A <see cref="Character"/> instance representing the newly created character.  The specific type of the
        /// character will be either <see cref="Warrior"/> or <see cref="Mage"/>  depending on the user's selection.</returns>
        public static Character CreateCharacter()
        {
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();

            Console.WriteLine("Choose your class: 1) Warrior  2) Mage");
            string input = Console.ReadLine()?.ToLower();

            return input switch
            {
                "1" or "warrior" => new Warrior(name, EquipableSelectionMenu()),
                "2" or "mage" => new Mage(name),
                _ => new Warrior(name, new List<IEquipable>()) // fallback
            };
        }

        /// <summary>
        /// Displays a menu for selecting equipable items and returns a list of the selected items.
        /// </summary>
        /// <remarks>The menu allows the user to select from a predefined set of equipable items, such as
        /// "Sword", "Bow",  "Quiver", "Axe", "Spear", and "Dagger". The user can add items until the maximum inventory
        /// space  of 4 slots is reached. Each item occupies either 1 or 2 slots depending on its size. The user can 
        /// exit the menu by entering 'q'.</remarks>
        /// <returns>A list of equipable items selected by the user. The list will contain zero or more items, depending  on the
        /// user's input.</returns>
        private static List<IEquipable> EquipableSelectionMenu()
        {
            List<IEquipable> equipables = new();
            int usedSlots = 0;
            const int maxSlots = 4;

            while (true)
            {
                Console.Write($"Add item (Sword, Bow, Quiver, Axe, Spear, Dagger or 'q' to finish) — {usedSlots}/{maxSlots}: ");
                string input = Console.ReadLine().ToLower();
                if (input == "q") break;

                try
                {
                    IEquipable item = input switch
                    {
                        "sword" => new Sword(),
                        "axe" => new Axe(),
                        "spear" => new Spear(),
                        "dagger" => new Dagger(),
                        "bow" => new SimpleBow(),
                        "quiver" => new SmallQuiver(),
                        _ => throw new InvalidWeaponException(input)
                    };

                    int itemSize = item.InventorySpaceAmount == InventorySpaceAmount.SMALL ? 1 : 2;

                    if (usedSlots + itemSize > maxSlots)
                    {
                        Console.WriteLine("Not enough space for that item!");
                        continue;
                    }

                    equipables.Add(item);
                    usedSlots += itemSize;
                    Console.WriteLine($"{item.Name} added!");
                }
                catch (InvalidWeaponException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return equipables;
        }
    }
}
