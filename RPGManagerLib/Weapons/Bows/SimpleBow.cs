namespace RPGManagerLib.Weapons.Bows
{
    internal class SimpleBow : Bow
    {
        public SimpleBow()
            : base(
                  damageAmount: 10,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Simple Bow",
                  weaponType: WeaponType.SIMPLEBOW,
                  element: Element.NONE,
                  cooldownTime: 2.2,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE) { }

        public SimpleBow(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, double cooldownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.SIMPLEBOW, element, cooldownTime, inventorySpaceAmount)
        {
        }
    }
}
