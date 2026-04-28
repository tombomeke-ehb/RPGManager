using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Items.Weapons.Quivers
{
    public class MediumQuiver : Quiver
    {
        public MediumQuiver()
            : base(name: "Medium Quiver",
                    rarity: Rarity.UNCOMMON,
                    inventorySpaceAmount: InventorySpaceAmount.SMALL,
                    capacity: 30,
                    variant: QuiverVariant.MEDIUM)
        {

        }
    }
}
