namespace RPGManagerLib.Items.Weapons.Melee.Axes
{
    public class BattleAxe : Axe
    {
        public BattleAxe()
            : base(
                  damageAmount: 26,
                  durability: 100,
                  rarity: Rarity.UNCOMMON,
                  level: 4,
                  name: "Battle Axe",
                  element: Element.NONE,
                  variant: AxeVariant.BATTLE,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
        }
    }
}
