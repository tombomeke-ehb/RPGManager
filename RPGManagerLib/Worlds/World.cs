using RPGManagerLib.Locations;

namespace RPGManagerLib.Worlds
{
    public class World
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DifficultyLevel { get; set; }
        public bool IsUnlocked { get; set; }
        public List<Location> Locations { get; private set; }

        // Default Constructor
        public World()
        {
            // Default values make sure your world is in a valid, usable state
            Name = "Unknown World";
            Description = "A mysterious, unexplored land.";
            DifficultyLevel = 1;
            IsUnlocked = false;
            Locations = new List<Location>();  // prevent null reference issues
        }

        public World(string name)
        {
            Name = name;
            Description = "A world waiting to be discovered.";
            DifficultyLevel = 1;
            IsUnlocked = false;
            Locations = new List<Location>();
        }

        public World(string name, string description)
        {
            Name = name;
            Description = description;
            DifficultyLevel = 1;
            IsUnlocked = false;
            Locations = new List<Location>();
        }

        public World(string name, string description, int difficultyLevel, bool isUnlocked, List<Location>? locations = null)
        {
            Name = name;
            Description = description;
            DifficultyLevel = difficultyLevel;
            IsUnlocked = isUnlocked;
            // If no locations provided, make an empty list (avoids null exceptions)
            Locations = locations ?? new List<Location>();
        }

        // Add a location to the Locations list
        public void AddLocation(Location location)
        {
            if (!Locations.Contains(location))
                Locations.Add(location);
        }

        // Remove a location from the Locations list
        public void RemoveLocation(Location location)
        {
            Locations.Remove(location);
        }

        /// <summary>
        /// Unlocks the current instance, allowing access to its features.
        /// </summary>
        /// <remarks>Once unlocked, the instance can be used without restrictions. Ensure that unlocking
        /// is performed only when necessary to maintain security or integrity.</remarks>
        public void UnLock() => IsUnlocked = true;

        /// <summary>
        /// Locks the current instance, denying access to its features.
        /// </summary>
        /// <remarks>Locking of a world only happens when for example losing rights to travel there (low reputation)
        /// or losing the items nescesary to travel to said world</remarks>
        public void Lock() => IsUnlocked = false;
    }
}
