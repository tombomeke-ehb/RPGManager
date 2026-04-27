namespace RPGManagerLib.Items.Weapons.Melee.Swords
{
    /// <summary>
    /// A balanced melee weapon with moderate damage and cooldown.
    /// </summary>
    public abstract class Sword : Weapon
    {


        public Sword(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, SwordVariant variant, InventorySpaceAmount inventorySpaceAmount)
            : base
                  (damageAmount: damageAmount,
                  durability: durability,
                  rarity: rarity,
                  level: level,
                  name: name,
                  weaponType: WeaponType.SWORD,
                  variant: variant,
                  element: element,
                  inventorySpaceAmount: inventorySpaceAmount)
        {
        }
    }
}
