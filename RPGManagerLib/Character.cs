using RPGManagerLib.Exceptions;

namespace RPGManagerLib
{
    /// <summary>
    /// Represents a character in the system with attributes such as name, health, creation date, and power level.
    /// </summary>
    /// <remarks>The <see cref="Character"/> class provides functionality to manage a character's state,
    /// including health management through healing and damage operations. It also allows for the initialization of
    /// characters with default or custom values and provides a textual representation of the character's current
    /// state.</remarks>
    public class Character
    {
        /// <summary>
        /// Represents the health value of an entity.
        /// </summary>
        /// <remarks>This field stores the current health of the entity as a double-precision
        /// floating-point value. The value is intended to be used internally to track the entity's health
        /// status.</remarks>
        public double Health { get; private set; }

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
        /// Initializes a new instance of the <see cref="Character"/> class with default values.
        /// </summary>
        public Character()
        {
            Name = "Unknown";
            Health = 100.0;
            CreationDate = DateTime.Now;
            PowerLevel = 1;
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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class with custom values.
        /// </summary>
        /// <param name="name">The character's name.</param>
        /// <param name="health">The initial health value (0–100).</param>
        /// <param name="creationDate">The creation date of the character.</param>
        /// <param name="powerLevel">The starting power level.</param>
        public Character(string name, double health, DateTime creationDate, int powerLevel)
        {
            this.Name = name;
            this.Health = health;
            this.CreationDate = creationDate;
            this.PowerLevel = powerLevel;
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
                if (points + Health > 100)
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

        /// <summary>
        /// Returns a formatted string describing the character's current state.
        /// </summary>
        /// <returns>
        /// A string containing the character's name, health, creation date, and power level.
        /// </returns>
        public override string ToString()
        {
            return $"\nYour Character:\nName: {Name}, Health: {Health}, Date created: {CreationDate}, Level: {PowerLevel}";
        }
    }
}
