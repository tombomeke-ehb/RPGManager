namespace RPGManagerLib.Items.Weapons.Melee.Swords
{
    public class BroadSword : Sword
    {
        public BroadSword()
            : base(
                  damageAmount: 18,
                  durability: 130,
                  rarity: Rarity.UNCOMMON,
                  level: 3,
                  name: "Broad Sword",
                  element: Element.NONE,
                  variant: SwordVariant.BROAD,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
        }
    }
}
