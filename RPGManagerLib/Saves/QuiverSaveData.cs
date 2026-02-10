using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Represents the data required to save and restore a quiver.
    /// </summary>
    public class QuiverSaveData
    {
        /// <summary>
        /// Display name of the quiver.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Rarity tier of the quiver.
        /// </summary>
        public Rarity Rarity { get; set; }

        /// <summary>
        /// Inventory footprint for this quiver.
        /// </summary>
        public InventorySpaceAmount InventorySpaceAmount { get; set; }

        /// <summary>
        /// Maximum number of arrows that can be stored.
        /// </summary>
        public int Capacity { get; set; }

        public QuiverSaveData() { }

        /// <summary>
        /// Creates save data from a quiver instance.
        /// </summary>
        public QuiverSaveData(Quiver quiver)
        {
            Name = quiver.Name;
            Rarity = quiver.Rarity;
            InventorySpaceAmount = quiver.InventorySpaceAmount;
            Capacity = quiver.Capacity;
        }

        /// <summary>
        /// Converts this save data back to a Quiver instance.
        /// </summary>
        public Quiver ToQuiver()
        {
            // For now, we only have SmallQuiver, but we can check by name or add a type discriminator later
            Quiver quiver = Name switch
            {
                "Small Quiver" => new SmallQuiver(),
                _ => throw new Exception($"Unknown quiver type: '{Name}'. Currently supported types: Small Quiver")
            };

            // Update properties in case they were modified
            quiver.Name = this.Name;
            quiver.Rarity = this.Rarity;
            quiver.InventorySpaceAmount = this.InventorySpaceAmount;
            quiver.Capacity = this.Capacity;

            return quiver;
        }
    }
}
