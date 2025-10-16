namespace RPGManagerLib
{
    public class Mage : Character
    {
        public double ManaBoost { get; set; }
        public override String CharacterType => "Mage";

        /// <summary>
        /// Initializes a new instance of the <see cref="Mage"/> class with default values.
        /// </summary>
        public Mage() : base()
        {
            ManaBoost = 50.0;
        }

        public Mage(double manaboost) : base()
        {
            ManaBoost = manaboost;
        }

        public Mage(string name) : base(name)
        {
            Name = name;
            ManaBoost = 50.0;
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