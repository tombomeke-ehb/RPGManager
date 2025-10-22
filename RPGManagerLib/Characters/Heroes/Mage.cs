using System.Xml.Linq;

namespace RPGManagerLib.Characters.Heroes
{
    /// <summary>
    /// A magic-focused character with an inherent mana boost.
    /// </summary>
    public class Mage : Character
    {
        /// <summary>
        /// Additional mana capacity or effectiveness granted to the mage.
        /// </summary>
        public double ManaBoost { get; set; }
        public override string CharacterType => "Mage";

        /// <summary>
        /// Initializes a new instance of the <see cref="Mage"/> class with default values.
        /// </summary>
        public Mage() : base()
        {
            ManaBoost = 50.0;
        }

        /// <summary>
        /// Initializes a new <see cref="Mage"/> with a custom name and default stats.
        /// </summary>
        public Mage(string name) : base(name, 100, DateTime.Now, 1)
        {
            ManaBoost = 50.0;
        }


        /// <summary>
        /// Initializes a new <see cref="Mage"/> with explicit properties.
        /// </summary>
        public Mage(string name, double health, DateTime creationDate, int powerLevel, double manaboost) : base(name, health, creationDate, powerLevel)
        {
            ManaBoost = manaboost;
        }

        /// <summary>
        /// Returns a formatted string describing the character's current state.
        /// </summary>
        /// <returns>
        /// A string containing the character's name, health, creation date, power level, and mana-boost.
        /// </returns>
        public override string ToString()
        {
            return $"\nYour Character:\nName: {Name}, Health: {Health}, Date created: {CreationDate}, Level: {PowerLevel}, ManaBoost {ManaBoost}";
        }
    }
}
