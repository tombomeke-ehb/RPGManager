namespace RPGManagerLib.Items.Weapons.Melee.Daggers
{
    /// <summary>
    /// A fast melee weapon with low damage and short cooldown.
    /// </summary>
    public class Dagger : Weapon
    {
        public DaggerVariant Variant { get; }

        protected Dagger(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, DaggerVariant variant, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount: damageAmount,
                  durability: durability,
                  rarity: rarity,
                  level: level,
                  name: name,
                  weaponType: WeaponType.DAGGER,
                  element: element,
                  inventorySpaceAmount: inventorySpaceAmount
            )
        {
            Variant = variant;
        }
    }
}
