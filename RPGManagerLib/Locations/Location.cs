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

        /// <summary>
        /// Adds a non-player character (NPC) to the current list of NPCs.
        /// </summary>
        /// <remarks>If the list of NPCs is not initialized, this method will create a new list before
        /// adding the NPC.</remarks>
        /// <param name="npc">The NPC to add to the list. Cannot be null.</param>
        public void AddNpc(NPC npc)
        {
            if (NPCs == null)
            {
                NPCs = new List<NPC>();
            }
            NPCs.Add(npc);
        }

        /// <summary>
        /// Removes the specified NPC from the collection of NPCs.
        /// </summary>
        /// <remarks>If the collection of NPCs is null, this method performs no action.</remarks>
        /// <param name="npc">The NPC to be removed from the collection. Must not be null.</param>
        public void RemoveNpc(NPC npc)
        {
            if (NPCs != null)
            {
                NPCs.Remove(npc);
            }
        }

        /// <summary>
        /// Displays the names of all non-player characters (NPCs) in the console.
        /// </summary>
        /// <remarks>This method iterates through the collection of NPCs and writes each NPC's name to the
        /// console output. If the NPC collection is null, the method performs no action.</remarks>
        public void GetNpcs()
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
