using RPGManagerLib.Characters;
using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Weapons;

namespace RPGManagerLib.UI
{
    public static class CharacterFactory
    {
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
