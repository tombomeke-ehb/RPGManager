namespace RPGManagerLib.Items.Weapons.Melee.Swords
{
    public class GreatSword : Sword
    {
        public GreatSword()
            : base(
                  damageAmount: 22,
                  durability: 110,
                  rarity: Rarity.UNCOMMON,
                  level: 4,
                  name: "Great Sword",
                  element: Element.NONE,
                  variant: SwordVariant.GREAT,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
        }
    }
}
