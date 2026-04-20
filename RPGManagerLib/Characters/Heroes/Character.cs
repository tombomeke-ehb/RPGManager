using RPGManagerLib.Exceptions;
using RPGManagerLib.Worlds;

namespace RPGManagerLib.Characters.Heroes
{
    /// <summary>
    /// Represents a character in the system with attributes such as name, health, creation date, and power level.
    /// </summary>
    /// <remarks>The <see cref="Character"/> class provides functionality to manage a character's state,
    /// including health management through healing and damage operations. It also allows for the initialization of
    /// characters with default or custom values and provides a textual representation of the character's current
    /// state.</remarks>
    public abstract class Character
    {
        // TODO: Introduce Level/Experience and shared combat-facing members here when Character implements the combat system contract.
        /// <summary>
        /// Represents the health value of an entity.
        /// </summary>
        /// <remarks>This field stores the current health of the entity as a double-precision
        /// floating-point value. The value is intended to be used internally to track the entity's health
        /// status.</remarks>
        public double Health { get; internal set; }

        /// <summary>
        /// Gets or sets the character's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the character was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the character's power level.
        /// </summary>
        public int PowerLevel { get; set; }

        /// <summary>
        /// Gets or sets the character's amount of gold
        /// </summary>
        public int Gold {  get; set; }
        /// <summary>
        /// Gets or sets the character's mana amount.
        /// </summary>
        public double Mana {  get; set; }

        /// <summary>
        /// Gets the maximum health used for display and healing bounds.
        /// </summary>
        public virtual double MaxHealth => 100.0;

        /// <summary>
        /// Gets the maximum mana used for display.
        /// </summary>
        public virtual double MaxMana => 100.0;

        public abstract string CharacterType { get; }
        public World? CurrentWorld { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class with default values.
        /// </summary>
        /// 
        public Character()
        {
            Name = "John";
            Health = 100.0;
            CreationDate = DateTime.Now;
            PowerLevel = 1;
            Gold = 50;
            Mana = 100;

        }

        /// <summary>
        /// Initializes a new instaces of the <see cref="Character"/> class with custom values.
        /// </summary>
        /// <param name="name">The character's name.</param>
        public Character(string name)
        {
            Name = name;
            Health = 100.0;
            CreationDate = DateTime.Now;
            PowerLevel = 1;
            Gold = 50;
            Mana = 100;
        }

        /// <summary>
        /// Increases the character's health by the specified number of points, up to a maximum of 100.
        /// </summary>
        /// <remarks>
        /// If <paramref name="points"/> would cause health to exceed 100, it is capped at 100.
        /// If <paramref name="points"/> is negative, the method does not modify health and writes a warning message to the console.
        /// </remarks>
        /// <param name="points">The number of health points to restore. Must be non-negative.</param>
        public void Heal(double points)
        {
            try { 
                if(points < 0)
                {
                    throw new NegativeHealException();
                }
                if (points + Health > MaxHealth)
                {
                    throw new OverhealException();
                }

                Health += points;
                Console.WriteLine($"Healed {points} health points");
            }
            catch (CharacterException ex)
            {
                Console.WriteLine($"Heal failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Reduces the character's health by the specified number of points.
        /// </summary>
        /// <remarks>
        /// If <paramref name="points"/> is negative, the method does not modify health and writes a warning message to the console.
        /// If the resulting health falls below zero, it is set to zero and a death message is displayed.
        /// </remarks>
        /// <param name="points">The amount of health to subtract. Must be non-negative.</param>
        public void Damage(double points)
        {
            try
            {
                if (points < 0) {
                    throw new NegativeDamageException();
                }

                if (Health - points < -100)
                {
                    throw new OverkillException();
                }

                Health -= points;

                if (Health <= 0)
                {
                    Console.WriteLine("Character has died.");
                }
                else
                {
                    Console.WriteLine($"Damaged {points} health points.");
                }
            }
            catch (CharacterException ex)
            {
                Console.WriteLine($"Damage failed: {ex.Message}");
            }
        }

        public void TravelTo(World world)
        {
            // TODO: Replace this with region travel rules that consult WorldState, unlock flags, and travel requirements.
            if (world.IsUnlocked)
            {
                CurrentWorld = world;
            }
            else
            {
                Console.WriteLine($"world {world} not unlocked!");
            }
        }

        // TODO: Add functionality to travel to different worlds, which may require additional properties and methods related to world management.

        /// <summary>
        /// Returns class-specific equipment as a formatted line, e.g. "Sword (Common), Dagger (Common)".
        /// Subclasses override this to list their weapons.
        /// </summary>
        protected virtual string GetEquipmentLine() => "none";

        /// <summary>
        /// Returns a formatted two-column stat block for this character.
        /// </summary>
        public override string ToString()
        {
            string header = $"--- {Name} the {CharacterType} ---";
            // The first two rows are laid out in two columns to resemble a character sheet.
            string row1 = $"  {"Level",-9}: {PowerLevel,-10}{"Health",-7}: {Health:0} / {MaxHealth:0}";
            string row2 = $"  {"Power",-9}: {PowerLevel,-10}{"Mana",-7}: {Mana:0} / {MaxMana:0}";
            string row3 = $"  {"Gold",-9}: {Gold}";
            string row4 = $"  Equipment: {GetEquipmentLine()}";
            return $"\n{header}\n{row1}\n{row2}\n{row3}\n{row4}";
        }
    }
}
//TODO: Research need for all constructors or only those to create a character
