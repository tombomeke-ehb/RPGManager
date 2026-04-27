using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Weapons.Melee
{
    public class GreatAxe : Axe
    {
        public GreatAxe()
            : base(
                  damageAmount: 32,
                  durability: 110,
                  rarity: Rarity.RARE,
                  level: 8,
                  name: "Great Axe",
                  element: Element.NONE,
                  variant: AxeVariant.GREAT,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
        }
    }
}
