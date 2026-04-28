using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Items.Weapons.Quivers
{
    /// <summary>
    /// Represents a small quiver designed to hold arrows, suitable for basic inventory needs.
    /// </summary>
    /// <remarks>This class inherits from the Quiver class and provides a standard implementation with
    /// predefined properties for name, rarity, inventory space, and capacity.</remarks>
    public class SmallQuiver : Quiver
    {

        /// <summary>
        /// Initializes a new instance of the SmallQuiver class with predefined properties for name, rarity, inventory
        /// space, and capacity.
        /// </summary>
        /// <remarks>This constructor creates a SmallQuiver item with the name "Small Quiver", a common
        /// rarity, small inventory space requirement, and a capacity of 15 arrows. Use this constructor to obtain a
        /// standard small quiver suitable for basic inventory needs.</remarks>
        public SmallQuiver()
            : base(name: "Small Quiver",
                    rarity: Rarity.COMMON,
                    inventorySpaceAmount: InventorySpaceAmount.SMALL,
                    capacity: 15,
                    variant: QuiverVariant.SMALL)
        { }
    }
}

