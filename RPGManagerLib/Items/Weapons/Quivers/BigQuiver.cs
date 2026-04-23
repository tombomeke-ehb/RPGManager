using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Items.Weapons.Quivers
{
    public class BigQuiver : Quiver
    {
        BigQuiver()
            : base(name: "Big Quiver",
                    rarity: Rarity.RARE,
                    inventorySpaceAmount: InventorySpaceAmount.LARGE,
                    capacity: 50)
        {
        }
    }
}
