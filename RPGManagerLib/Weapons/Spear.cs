namespace RPGManagerLib.Weapons
{
    public class Spear : Weapon
    {
        public Spear() : base
            (damageAmount: 17,
            durability: 120,
            rarity: Rarity.COMMON,
            level: 1,
            name: "Basic Spear",
            weaponType: WeaponType.SPEAR,
            element: Element.NONE,
            cooldownTime: 2,
            inventorySpaceAmount: InventorySpaceAmount.LARGE) { }

        public Spear(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, double cooldownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.SPEAR, element, cooldownTime, inventorySpaceAmount)
        {
        }
    }
}
