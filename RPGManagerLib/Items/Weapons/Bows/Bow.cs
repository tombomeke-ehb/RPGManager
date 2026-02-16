namespace RPGManagerLib.Items.Weapons.Bows
{
    /// <summary>
    /// Base type for bow-style ranged weapons.
    /// </summary>
    public abstract class Bow : Weapon
    {
        public Bow(int damageAmount, int durability, Rarity rarity, int level, string name, WeaponType weaponType, Element element, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, weaponType, element, inventorySpaceAmount)
        {
        }
    }
}