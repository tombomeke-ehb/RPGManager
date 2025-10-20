using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Weapons.Quivers
{
    /// <summary>
    /// Base type for quivers that store ammunition for bows.
    /// </summary>
    public abstract class Quiver : IEquipable
    {
        /// <summary>
        /// Display name of the quiver.
        /// </summary>
        public string Name { get; set; }

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

        /// <summary>
        /// The item category, always <see cref="EquipableType.QUIVER"/> for quivers.
        /// </summary>
        public EquipableType EquipableType => EquipableType.QUIVER;

        /// <summary>
        /// Initializes a new quiver with explicit properties.
        /// </summary>
        protected Quiver(string name, Rarity rarity, InventorySpaceAmount inventorySpaceAmount, int capacity)
        {
            Name = name;
            Rarity = rarity;
            InventorySpaceAmount = inventorySpaceAmount;
            Capacity = capacity;
        }
    }
}
