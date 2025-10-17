namespace RPGManagerLib.Weapons
{
    public class Spear : Weapon
    {
        public Spear() : base
            (17,
            120,
            Rarity.COMMON,
            1,
            "Basic Spear",
            WeaponType.SPEAR,
            Element.NONE,
            2,
            InventorySpaceAmount.LARGE) { }

        public Spear(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, double cooldownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.SPEAR, element, cooldownTime, inventorySpaceAmount)
        {
        }
    }
}
