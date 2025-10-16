using RPGManagerLib.Weapons;

namespace RPGManagerLib
{
    /// <summary>
    /// Represents a warrior character with a collection of weapons.
    /// </summary>
    /// <remarks>The <see cref="Warrior"/> class extends the <see cref="Character"/> class, adding
    /// functionality for managing a list of weapons. It provides constructors for initializing a warrior with default
    /// or specific attributes, and overrides the <see cref="ToString"/> method to provide a detailed description of the
    /// warrior.</remarks>
    public class Warrior : Character
    {
        /// <summary>
        /// Gets or sets the collection of weapon names.
        /// </summary>
        public List<Weapon> Weapons { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Warrior"/> class.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="Warrior"/> instance and sets up the
        /// default state, including an empty list of weapons.</remarks>
        /// 
        public override string CharacterType => "Warrior";
        public Warrior() : base()
        {
            Weapons = new List<Weapon>();
        }

        /// <summary>
        /// Initializes a new instaces of the <see cref="Warrior"/> class with a name value and list of weapons
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weapons"></param>
        public Warrior(string name, List<Weapon> weapons)
            : base(name)
        {
            Weapons = weapons ?? new List<Weapon>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Warrior"/> class with custom values.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="creationDate"></param>
        /// <param name="powerLevel"></param>
        /// <param name="weapons"></param>
        public Warrior(string name, double health, DateTime creationDate, int powerLevel, List<Weapon> weapons)
            : base(name, health, creationDate, powerLevel)
        {
            Weapons = weapons ?? new List<Weapon>();
        }

        /// <summary>
        /// Returns a formatted string describing the character's current state.
        /// </summary>
        /// <returns>
        /// A string containing the character's name, health, creation date, power level and list of weapons.
        /// </returns>
        public override string ToString()
        {
            return $"\nYour Character:\nName: {Name}, Health: {Health}, Date created: {CreationDate}, Level: {PowerLevel}, Weapons: {Weapons}";
        }
    }
}
