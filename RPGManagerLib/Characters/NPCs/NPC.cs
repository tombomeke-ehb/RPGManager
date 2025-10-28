using RPGManagerLib.Locations;

namespace RPGManagerLib.Characters.NPCs
{
    public abstract class NPC
    {
        public string Name { get; set; }
        public Location Location;

        public NPC(string name, Location location)
        {
            Name = name;
            Location = location;
        }
    }
}
