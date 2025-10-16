namespace RPGManagerLib.Weapons
{
    public class Sword : Weapon
    {
        public Sword()
            : base(15, 100, Rarity.COMMON, 1, "Basic Sword", WeaponType.SWORD, Element.NONE, InventorySpaceAmount.SMALL)
        { }

        public Sword(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.SWORD, element, inventorySpaceAmount)
        { }
    }
}
