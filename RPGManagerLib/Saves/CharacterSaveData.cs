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
        /// Creates save data from a live <see cref="Character"/> instance.
        /// </summary>
        public CharacterSaveData(Character c)
        {
            Name = c.Name;
            Health = c.Health;
            CreationDate = c.CreationDate;
            PowerLevel = c.PowerLevel;
            CharacterType = c.CharacterType;
            Gold = c.Gold;

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
                Mana = m.Mana;
            }
            else if (c is Archer a)
            {
                Equipables = ConvertEquipables(a.Weapons);
            }
        }

        /// <summary>
        /// Reconstructs a live <see cref="Character"/> from this save data.
        /// </summary>
        public Character ToCharacter()
        {
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
                m.Mana = this.Mana;
            }

            return c;
        }
    }
}