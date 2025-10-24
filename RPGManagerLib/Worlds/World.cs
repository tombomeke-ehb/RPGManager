using RPGManagerLib.Locations;

namespace RPGManagerLib.Worlds
{
    public class World
    {
        public string Name { get; set; }
        public List<Location>? Locations;

        public World(string name)
        {
            Name = name;
        }

        public World(string name, List<Location> locations)
        {
            Name = name;
            Locations = locations;
        }
    }
}
