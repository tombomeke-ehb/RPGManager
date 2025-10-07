namespace RPGManagerLib
{
    public class Character
    {
        public string name { get; set; }
        private double health;
        public DateTime creationDate { get; set; }
        public int powerLevel { get; set; }

        public Character()
        {
            name = "Unknown";
            health = 100.0;
            creationDate = DateTime.Now;
            powerLevel = 1;
        }

        public Character(double health)
        {
            name = "Unknown";
            this.health = health;
            powerLevel = 1;
        }

        public Character(string name, double health, DateTime creationDate, int powerLevel)
        {
            this.name = name;
            this.health = health;
            this.creationDate = creationDate;
            this.powerLevel = powerLevel;
        }

        public void Heal(double points)
        {
            if (points < 0)
            {
                Console.WriteLine("Cannot heal negative points");
            }
            else if (points + health > 100)
            {
                Console.WriteLine("Health cannot exceed 100 points");
            }
            else
            {
                health += points;
                Console.WriteLine($"Healed {points} health points");
            }
        }

        public void Damage(double points)
        {
                    if (points < 0)
            {
                Console.WriteLine("Cannot damage negative points");
            }
            else if (health - points < 0)
            {
                health = 0;
                Console.WriteLine("Character is dead");
            }
            else
            {
                health -= points;
                Console.WriteLine($"Damaged {points} health points");
            }
        }

        public override string? ToString()
        {
            return $"\nYour Character:\nName: {name}, Health: {health}, Date created: {creationDate}, Level: {powerLevel}";
        }
    }
}
