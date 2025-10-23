using RPGManagerLib.Characters.NPCs;

namespace RPGManagerLib.Locations
{
    /// <summary>
    /// Represents a place in the world that can host NPCs and interactions.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Display name of the location (e.g., "Oakheart").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// NPCs currently present at this location.
        /// </summary>
        public List<NPC> NPCs { get; } = new();

        /// <summary>
        /// Initializes a new location with the given name.
        /// </summary>
        public Location(string name)
        {
            Name = name;
        }
    }
}
