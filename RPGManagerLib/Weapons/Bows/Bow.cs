namespace RPGManagerLib.Weapons.Bows
{
    public abstract class Bow : Weapon
    {
        protected Bow(int damageAmount, int durability, Rarity rarity, int level, string name, WeaponType weaponType, Element element, double cooldownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, weaponType, element, cooldownTime, inventorySpaceAmount)
        {
        }
    }
}
