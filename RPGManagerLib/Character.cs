namespace RPGManagerLib
{
    /// <summary>
    /// Represents a character in the RPG system, containing attributes such as name, health, creation date, and power level.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Represents the health value of an entity.
        /// </summary>
        /// <remarks>This field stores the current health of the entity as a double-precision
        /// floating-point value. The value is intended to be used internally to track the entity's health
        /// status.</remarks>
        private double Health;

        /// <summary>
        /// Gets the current health value of the entity.
        /// </summary>
        public double health => Health;

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
        /// Initializes a new instance of the <see cref="Character"/> class with custom values.
        /// </summary>
        /// <param name="Name">The character's name.</param>
        /// <param name="Health">The initial health value (0–100).</param>
        /// <param name="CreationDate">The creation date of the character.</param>
        /// <param name="PowerLevel">The starting power level.</param>
        public Character(string Name, double Health, DateTime CreationDate, int PowerLevel)
        {
            this.Name = Name;
            this.Health = Health;
            this.CreationDate = CreationDate;
            this.PowerLevel = PowerLevel;
        }

        /// <summary>
        /// Increases the character's health by the specified number of points, up to a maximum of 100.
        /// </summary>
        /// <remarks>
        /// If <paramref name="Points"/> would cause health to exceed 100, it is capped at 100.
        /// If <paramref name="Points"/> is negative, the method does not modify health and writes a warning message to the console.
        /// </remarks>
        /// <param name="Points">The number of health points to restore. Must be non-negative.</param>
        public void Heal(double Points)
        {
            if (Points < 0)
            {
                Console.WriteLine("Cannot heal negative points.");
            }
            else if (Points + Health > 100)
            {
                Health = 100;
                Console.WriteLine("Health capped at 100 points.");
            }
            else
            {
                Health += Points;
                Console.WriteLine($"Healed {Points} health points.");
            }
        }

        /// <summary>
        /// Reduces the character's health by the specified number of points.
        /// </summary>
        /// <remarks>
        /// If <paramref name="Points"/> is negative, the method does not modify health and writes a warning message to the console.
        /// If the resulting health falls below zero, it is set to zero and a death message is displayed.
        /// </remarks>
        /// <param name="Points">The amount of health to subtract. Must be non-negative.</param>
        public void Damage(double Points)
        {
            if (Points < 0)
            {
                Console.WriteLine("Cannot damage negative points.");
            }
            else if (Health - Points <= 0)
            {
                Health = 0;
                Console.WriteLine("Character has died.");
            }
            else
            {
                Health -= Points;
                Console.WriteLine($"Damaged {Points} health points.");
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
