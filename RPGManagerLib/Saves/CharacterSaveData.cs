using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Serializable snapshot of a character for saving and loading.
    /// </summary>
    public class CharacterSaveData
    {
        // TODO: Add Level and Experience here when the leveling system replaces the current PowerLevel-only progression.
        /// <summary>
        /// Discriminator describing the concrete character type (e.g., Warrior, Mage).
        /// </summary>
        public string CharacterType { get; set; } = "";

        /// <summary>
        /// Character name.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Current health value.
        /// </summary>
        public double Health { get; set; }

        /// <summary>
        /// Original creation date of the character.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Current power level.
        /// </summary>
        public int PowerLevel { get; set; }

        /// <summary>
        /// Serialized equipable items (Weapons, Quivers, etc).
        /// Using polymorphism to keep JSON clean.
        /// </summary>
        public List<EquipableSaveData> Equipables { get; set; } = new();

        /// <summary>
        /// Mana boost value for mages.
        /// </summary>
        public double Mana { get; set; } // alleen voor Mage

        public int Gold { get; set; }

        public CharacterSaveData() { }

        /// <summary>
        /// Initializes a new instance of the CharacterSaveData class using the specified Character object, capturing
        /// its properties for saving.
        /// </summary>
        /// <remarks>This constructor handles different character types (Warrior, Mage, Archer) and
        /// populates the Equipables or Mana properties accordingly. Ensure that the Character object provided is valid
        /// and contains the necessary data.</remarks>
        /// <param name="c">The Character object containing the data to be saved, including its name, health, creation date, power
        /// level, character type, and gold amount.</param>
        /// <exception cref="Exception">Thrown if an unknown item type is encountered during the conversion of equipable items.</exception>
        public CharacterSaveData(Character c)
        {
            Name = c.Name;
            Health = c.Health;
            CreationDate = c.CreationDate;
            PowerLevel = c.PowerLevel;
            CharacterType = c.CharacterType;
            Gold = c.Gold;

            // TODO: Extend this conversion when full inventory, spellbook, and other per-character systems need persistence.
            // Helper function to convert list of IEquipable to correct SaveData type
            List<EquipableSaveData> ConvertEquipables(IEnumerable<IEquipable> items)
            {
                return items.Select(item =>
                {
                    if (item is Weapon weapon) return (EquipableSaveData)new WeaponSaveData(weapon);
                    if (item is Quiver quiver) return (EquipableSaveData)new QuiverSaveData(quiver);
                    throw new Exception($"Unknown item type: {item.GetType().Name}");
                }).ToList();
            }

            if (c is Warrior w)
            {
                Equipables = ConvertEquipables(w.Weapons);
            }
            else if (c is Mage m)
            {
                Equipables = ConvertEquipables(m.Weapons);
                Mana = m.Mana;
            }
            else if (c is Archer a)
            {
                Equipables = ConvertEquipables(a.Weapons);
            }
        }

        /// <summary>
        /// Creates a new character instance based on the current save data, initializing its properties according to
        /// the specified character type.
        /// </summary>
        /// <remarks>The method sets the character's health, creation date, power level, and gold. For
        /// Warrior and Archer types, it populates the weapons collection from the equipables in the save data. For Mage
        /// characters, it sets the mana value. This method is typically used to reconstruct a character from saved
        /// data.</remarks>
        /// <returns>A new instance of the Character class, which may be a Warrior, Archer, or Mage, populated with the current
        /// save data's attributes.</returns>
        /// <exception cref="Exception">Thrown if the character type specified in the save data is not recognized.</exception>
        public Character ToCharacter()
        {
            // TODO: Restore Level/Experience and other new subsystems here as CharacterSaveData grows.
            Character c = CharacterType switch
            {
                "Warrior" => new Warrior(Name),
                "Archer" => new Archer(Name),
                "Mage" => new Mage(Name),
                _ => throw new Exception($"Unknown character type: {CharacterType}")
            };

            c.Health = this.Health;
            c.CreationDate = this.CreationDate;
            c.PowerLevel = this.PowerLevel;
            c.Gold = this.Gold;

            if (c is Warrior w)
            {
                w.Weapons.Clear();
                // Polymorphism handles ToEquipable() automatically!
                w.Weapons.AddRange(this.Equipables.Select(e => e.ToEquipable()));
            }
            else if (c is Archer a)
            {
                a.Weapons.Clear();
                a.Weapons.AddRange(this.Equipables.Select(e => e.ToEquipable()));
            }
            else if (c is Mage m)
            {
                m.Weapons.Clear();
                m.Weapons.AddRange(this.Equipables.Select(e => e.ToEquipable()));
                m.Mana = this.Mana;
            }

            return c;
        }
    }
}
