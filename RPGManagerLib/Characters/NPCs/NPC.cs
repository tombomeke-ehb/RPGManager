using RPGManagerLib.Locations;

namespace RPGManagerLib.Characters.NPCs
{
    public abstract class NPC
    {
        public string name { get; set; }
        public Location location;

        public NPC(string name, Location location)
        {
            this.name = name;
            this.location = location;
        }
    }
}
