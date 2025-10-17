using System.Xml.Linq;

namespace RPGManagerLib.Characters
{
    public class Mage : Character
    {
        public double ManaBoost { get; set; }
        public override string CharacterType => "Mage";

        /// <summary>
        /// Initializes a new instance of the <see cref="Mage"/> class with default values.
        /// </summary>
        public Mage() : base()
        {
            ManaBoost = 50.0;
        }

        public Mage(string name) : base(name, 100, DateTime.Now, 1)
        {
            ManaBoost = 50.0;
        }


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