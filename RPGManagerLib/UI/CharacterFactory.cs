using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items;
using RPGManagerLib.Items.Staffs;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items.Weapons.Melee.Axes;
using RPGManagerLib.Items.Weapons.Melee.Daggers;
using RPGManagerLib.Items.Weapons.Melee.Swords;
using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Spells;

namespace RPGManagerLib.UI
{
    /// <summary>
    /// Provides functionality to create character instances based on user input.
    /// </summary>
    public static class CharacterFactory
    {
        /// <summary>
        /// Creates a new character based on user input.
        /// </summary>
        public static Character CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("--- Create Your Hero ---\n");

            Console.Write("  Enter your hero's name: ");
            string? name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "John";
            }

            Console.WriteLine("\n\n  Choose a class:\n");
            WriteSelectionLine("1", "Warrior", "frontline fighter, high health, melee weapons");
            WriteSelectionLine("2", "Mage", "arcane spellcaster, high mana, staff and spells");
            WriteSelectionLine("3", "Archer", "ranged attacker, balanced stats, bow and quiver");

            Console.Write("\n  > ");
            string? input = Console.ReadLine()?.Trim().ToLowerInvariant();

            Character character = input switch
            {
                "1" or "warrior" => CreateWarrior(name),
                "2" or "mage" => CreateMage(name),
                "3" or "archer" => CreateArcher(name),
                _ => CreateWarrior(name)
            };

            Console.WriteLine($"\n\n  Hero {character.Name} created.");
            return character;
        }

        /// <summary>
        /// Creates a list of default weapons suitable for a warrior character.
        /// </summary>
        public static List<IEquipable> CreateDefaultWeaponsWarrior()
        {
            return new List<IEquipable> { new BasicSword() };
        }

        /// <summary>
        /// Creates a list of default weapons suitable for an archer character.
        /// </summary>
        public static List<IEquipable> CreateDefaultWeaponsArcher()
        {
            return new List<IEquipable>
            {
                new BasicDagger(),
                new SimpleBow(),
                new SmallQuiver()
            };
        }

        /// <summary>
        /// Creates a list of default weapons suitable for a mage character.
        /// </summary>
        public static List<IEquipable> CreateDefaultWeaponsMage()
        {
            return new List<IEquipable>
            {
                new BasicDagger(),
                new BasicStaff()
            };
        }

        /// <summary>
        /// Displays a menu for selecting a warrior's starting equipment and returns the selected items.
        /// </summary>
        private static List<IEquipable> ChooseWarriorStartingEquipment()
        {
            // Each entry defines the menu key, display text, slot cost, and the item object to create.
            var options = new (string Key, string Label, string Hint, int Slots, Func<IEquipable> CreateItem)[]
            {
                ("1", "Sword", "balanced melee weapon", 1, () => new BasicSword()),
                ("2", "Axe", "heavy melee, high damage", 2, () => new BasicAxe()),
                ("3", "Dagger", "fast, low damage", 1, () => new BasicDagger()),
                ("4", "Spear", "reach weapon, solid durability", 2, () => new Spear())
            };

            const int maxSlots = 4;

            while (true)
            {
                Console.WriteLine($"\n\n  Choose your starting equipment ({maxSlots} inventory slots):\n");

                foreach (var option in options)
                {
                    string slotLabel = option.Slots == 1 ? "slot" : "slots";
                    Console.WriteLine($"    [{option.Key}] {option.Label.PadRight(10)} ({option.Slots} {slotLabel})  — {option.Hint}");
                }

                Console.Write("\n  > ");
                string input = Console.ReadLine()?.Trim() ?? string.Empty;
                string[] selections = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (selections.Length == 0)
                {
                    Console.WriteLine("\n  Choose at least one item.");
                    continue;
                }

                var chosenOptions = new List<(string Key, string Label, string Hint, int Slots, Func<IEquipable> CreateItem)>();
                bool invalidSelection = false;

                foreach (string selection in selections)
                {
                    var option = options.FirstOrDefault(opt => opt.Key == selection);
                    if (string.IsNullOrEmpty(option.Key))
                    {
                        Console.WriteLine($"\n  '{selection}' is not a valid equipment choice.");
                        invalidSelection = true;
                        break;
                    }

                    chosenOptions.Add(option);
                }

                if (invalidSelection)
                {
                    continue;
                }

                int usedSlots = chosenOptions.Sum(option => option.Slots);
                if (usedSlots > maxSlots)
                {
                    Console.WriteLine($"\n  That loadout uses {usedSlots} slots. Choose {maxSlots} or fewer.");
                    continue;
                }

                // Build fresh item instances from the selected menu entries.
                return chosenOptions.Select(option => option.CreateItem()).ToList();
            }
        }

        private static Warrior CreateWarrior(string name)
        {
            var warrior = new Warrior(name);
            warrior.Weapons.AddRange(ChooseWarriorStartingEquipment());
            return warrior;
        }

        private static Archer CreateArcher(string name)
        {
            var archer = new Archer(name);
            archer.Weapons.AddRange(CreateDefaultWeaponsArcher());
            return archer;
        }

        private static Mage CreateMage(string name)
        {
            var mage = new Mage(name);
            mage.Weapons.AddRange(CreateDefaultWeaponsMage());
            mage.Spells.Add(new Fireball());
            mage.Spells.Add(new IceSpike());
            return mage;
        }

        private static void WriteSelectionLine(string key, string label, string hint)
        {
            Console.WriteLine($"    [{key}] {label.PadRight(9)} — {hint}");
        }
    }
}
