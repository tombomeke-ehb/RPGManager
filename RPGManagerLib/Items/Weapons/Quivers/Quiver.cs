using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Weapons.Quivers
{
    /// <summary>
    /// Represents an abstract base class for quivers, which are used to store and manage arrows in an inventory system.
    /// </summary>
    /// <remarks>This class provides properties for the quiver's display name, rarity, inventory footprint,
    /// and capacity. Derived classes should implement specific behaviors and attributes for different types of
    /// quivers.</remarks>
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
        /// Initializes a new instance of the Quiver class with the specified name, rarity, inventory space amount, and
        /// capacity.
        /// </summary>
        /// <remarks>Use this constructor to create a quiver with custom attributes for inventory
        /// management and item capacity.</remarks>
        /// <param name="name">The unique name that identifies the quiver.</param>
        /// <param name="rarity">The rarity level of the quiver, which determines its quality and value.</param>
        /// <param name="inventorySpaceAmount">The amount of inventory space that the quiver occupies.</param>
        /// <param name="capacity">The maximum number of items that the quiver can hold.</param>
        protected Quiver(string name, Rarity rarity, InventorySpaceAmount inventorySpaceAmount, int capacity)
        {
            Name = name;
            Rarity = rarity;
            InventorySpaceAmount = inventorySpaceAmount;
            Capacity = capacity;
        }
    }
}
