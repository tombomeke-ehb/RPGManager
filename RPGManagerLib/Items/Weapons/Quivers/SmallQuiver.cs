using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Items.Weapons.Quivers
{
    public class SmallQuiver : Quiver
    {

        public SmallQuiver()
            : base(name: "Small Quiver",
                    rarity: Rarity.COMMON,
                    inventorySpaceAmount: InventorySpaceAmount.SMALL,
                    capacity: 15)
        { }
        public SmallQuiver(string name, Rarity rarity, InventorySpaceAmount inventorySpaceAmount, int capacity)
            : base(name, rarity, inventorySpaceAmount, capacity)
        {
        }
    }
}
