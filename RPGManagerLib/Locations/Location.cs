using RPGManagerLib.Characters.NPCs;

namespace RPGManagerLib.Locations
{
    public class Location
    {
        public string Name { get; set; }
        public List<NPC> NPCs { get; set; }

        public Location(string name)
        {
            Name = name;
            NPCs = new List<NPC>();
        }

        public Location(string name, List<NPC> NPCs)
        {
            Name = name;
            this.NPCs = NPCs ?? new List<NPC>();
        }

        /// <summary>
        /// Adds a non-player character (NPC) to the current list of NPCs.
        /// </summary>
        /// <param name="npc">The NPC to add to the list. Cannot be null.</param>
        public void AddNPC(NPC npc)
        {
            NPCs.Add(npc);
        }

        /// <summary>
        /// Removes the specified NPC from the collection of NPCs.
        /// </summary>
        /// <param name="npc">The NPC to be removed from the collection. Must not be null.</param>
        public void RemoveNPC(NPC npc)
        {
            NPCs.Remove(npc);
        }

        /// <summary>
        /// Displays the names of all non-player characters (NPCs) in the console.
        /// </summary>
        /// <remarks>This method iterates through the collection of NPCs and writes each NPC's name to the
        /// console output.</remarks>
        public void GetNPCs()
        {
            foreach (NPC npc in NPCs)
            {
                Console.WriteLine(npc.Name);
            }
        }
    }
}
