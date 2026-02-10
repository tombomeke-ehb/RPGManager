using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items;

namespace RPGManagerLib.Characters.Heroes
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
        public List<IEquipable> Weapons { get; set; } = new List<IEquipable>();
        public override string CharacterType => "Warrior";

        /// <summary>
        /// Initializes a new instance of the <see cref="Warrior"/> class.
        /// </summary>
        /// <remarks>This constructor creates a Warrior with an empty list of weapons. Use this
        /// constructor when you want to create a Warrior without specifying initial equipment or name, the <see cref="Character"/> class will provide default values and the Warrior will have the name "John".</remarks>
        public Warrior() : base() { }

        /// <summary>
        /// Initializes a new instaces of the <see cref="Warrior"/> class with a name value and default values for other properties.
        /// </summary>
        /// <param name="name"></param>
        public Warrior(string name) : base(name) { }

        /// <summary>
        /// Returns a formatted string describing the character's current state.
        /// </summary>
        /// <returns>
        /// A string containing the character's name, health, creation date, power level and list of weapons.
        /// </returns>
        public override string ToString()
        {
            // Gebruik de ToString van Character als basis, als je dat wilt behouden
            string baseInfo = base.ToString();

            // Toon de lijst met wapens leesbaar
            string weaponList = Weapons != null && Weapons.Any()
                ? string.Join(", ", Weapons.Select(w => w.Name))
                : "none";

            return $"{baseInfo}\nWeapons: {weaponList}";
        }

    }
}
