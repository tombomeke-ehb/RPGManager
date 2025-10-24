using RPGManagerLib.Characters.NPCs;

namespace RPGManagerLib.Locations
{
    public class Location
    {
        public string name;
        public List<NPC>? NPCs;

        public Location(string name)
        {
            this.name = name;
        }

        public Location(string name, List<NPC> NPCs)
        {
            this.name = name;
            this.NPCs = NPCs;
        }

        public void addNPC(NPC npc)
        {
            if (NPCs == null)
            {
                NPCs = new List<NPC>();
            }
            NPCs.Add(npc);
        }

        public void removeNPC(NPC npc)
        {
            if (NPCs != null)
            {
                NPCs.Remove(npc);
            }
        }

        public void getNPCs()
        {
            if (NPCs != null)
            {
                foreach (NPC npc in NPCs)
                {
                    Console.WriteLine(npc.Name);
                }
            }
        }
    }
}
