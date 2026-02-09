using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Items.Weapons.Quivers
{
    /// <summary>
    /// A small quiver with limited capacity and minimal inventory footprint.
    /// </summary>
    public class SmallQuiver : Quiver
    {

        /// <summary>
        /// Initializes a new <see cref="SmallQuiver"/> with default values.
        /// </summary>
        public SmallQuiver()
            : base(name: "Small Quiver",
                    rarity: Rarity.COMMON,
                    inventorySpaceAmount: InventorySpaceAmount.SMALL,
                    capacity: 15)
        { }
    }
}

