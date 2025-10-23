using RPGManagerLib.Characters.NPCs;
using RPGManagerLib.Locations;

namespace RPGManagerLib.World
{
    /// <summary>
    /// Minimal world bootstrap with a few locations and NPCs.
    /// </summary>
    public static class World
    {
        private static bool initialized = false;
        private static readonly List<Location> locations = new();

        public static IReadOnlyList<Location> Locations
        {
            get { EnsureInitialized(); return locations; }
        }

        public static void EnsureInitialized()
        {
            if (initialized) return;

            var town = new Location("Oakheart");
            var smith = new BlackSmith(town);
            town.NPCs.Add(smith);

            locations.Add(town);
            initialized = true;
        }
    }
}

