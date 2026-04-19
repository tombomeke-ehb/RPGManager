using RPGManagerLib.Items;
using RPGManagerLib.Spells;

namespace RPGManagerLib.Characters.Heroes
{
    /// <summary>
    /// A magic-focused character with an inherent mana boost.
    /// </summary>
    public class Mage : Character
    {
        /// <summary>
        /// List of spells known by the mage. Mages can cast these spells using their mana.
        /// </summary>
        public List<Spell> Spells { get; set; } = new();
        /// <summary>
        /// List of weapons equipped by the mage. Mages typically favor staves and magical implements, but this list can include any equipable item categorized as a weapon.
        /// </summary>
        public List<IEquipable> Weapons { get; set; } = new List<IEquipable>();
        /// <summary>
        /// Gets the character type for this hero.
        /// </summary>
        public override string CharacterType => "Mage";

        /// <summary>
        /// Initializes a new instance of the <see cref="Mage"/> class with default values.
        /// </summary>
        public Mage() : base()
        {
            Mana = 150.0; // Mage has 50 mana points extra then other characters by default
        }

        /// <summary>
        /// Initializes a new <see cref="Mage"/> with a custom name and default stats.
        /// </summary>
        /// <param name="name">The name of the mage.</param>
        public Mage(string name) : base(name)
        {
            Mana = 150.0; // Mage has 50 mana points extra then other characters by default
        }

        /// <summary>
        /// Returns a formatted string describing the character's current state.
        /// </summary>
        /// <returns>
        /// A string containing the character's name, health, creation date, power level, and mana-boost.
        /// </returns>
        public override string ToString()
        {
            return $"\nYour Character:\nName: {Name}, Health: {Health}, Date created: {CreationDate}, Level: {PowerLevel}, Mana {Mana}";
        }

        //TODO: Implement spells and mana system (ESP)


        public void CastSpell(int i, Character target)
        {
            if (i < 0 || i >= Spells.Count)
            {
                Console.WriteLine("Invalid spell.");
                return;
            }

            Spells[i].Cast(this, target);
        }

    }
}
