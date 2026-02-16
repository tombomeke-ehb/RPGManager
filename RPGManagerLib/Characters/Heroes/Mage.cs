using RPGManagerLib.Items;

namespace RPGManagerLib.Characters.Heroes
{
    /// <summary>
    /// A magic-focused character with an inherent mana boost.
    /// </summary>
    public class Mage : Character
    {
        /// <summary>
        /// Mana Capacity
        /// </summary>
        public List<IEquipable> Weapons { get; set; } = new List<IEquipable>();
        public override string CharacterType => "Mage";

        /// <summary>
        /// Initializes a new instance of the <see cref="Mage"/> class with default values.
        /// </summary>
        public Mage() : base()
        {
            Mana = 150.0; // Mage has 50 man a points extra then other characters by default
        }

        /// <summary>
        /// Initializes a new <see cref="Mage"/> with a custom name and default stats.
        /// </summary>
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
    }
}
