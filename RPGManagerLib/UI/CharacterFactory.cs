using RPGManagerLib.Characters;
using RPGManagerLib.Weapons;
using RPGManagerLib.Weapons.Melee;
using RPGManagerLib.Weapons.Bows;

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
                "1" or "warrior" => new Warrior(name, WeaponSelectionMenu()),
                "2" or "mage" => new Mage(name),
                _ => new Warrior(name, new List<Weapons.Weapon>()) // fallback
            };
        }

        private static List<Weapons.Weapon> WeaponSelectionMenu()
        {
            List<Weapons.Weapon> weapons = new();
            int usedSlots = 0;
            const int maxSlots = 4;

            while (true)
            {
                Console.Write($"Add weapon (Sword, Bow, Staff, Axe, Spear, Dagger or 'q' to finish) — {usedSlots}/{maxSlots}: ");
                string input = Console.ReadLine().ToLower();
                if (input == "q") break;

                try
                {
                    Weapon weapon = input switch
                    {
                        "sword" => new Sword(),
                        "axe" => new Axe(),
                        "spear" => new Spear(),
                        "dagger" => new Dagger(),
                        "bow" => new SimpleBow(),
                        _ => throw new Weapons.InvalidWeaponException(input)
                    };

                    int weaponSize = weapon.InventorySpaceAmount == Weapons.InventorySpaceAmount.SMALL ? 1 : 2;
                    if (usedSlots + weaponSize > maxSlots)
                    {
                        Console.WriteLine("Not enough space for that weapon!");
                        continue;
                    }

                    weapons.Add(weapon);
                    usedSlots += weaponSize;
                    Console.WriteLine($"{weapon.Name} added!");
                }
                catch (Weapons.InvalidWeaponException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return weapons;
        }
    }
}
