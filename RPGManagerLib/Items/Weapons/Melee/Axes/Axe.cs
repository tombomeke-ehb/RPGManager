namespace RPGManagerLib.Items.Weapons.Melee.Axes
{
    /// <summary>
    /// Represents a melee weapon of type axe with predefined damage, durability, rarity, and inventory space
    /// attributes.
    /// </summary>
    /// <remarks>The <see cref="Axe"/> class inherits from <see cref="Weapon"/> and is initialized with
    /// default values suitable for entry-level gameplay. It is categorized as a common item, deals moderate damage, and
    /// occupies a large inventory space. This class is intended for use by players at level 1 and does not possess
    /// elemental properties.</remarks>
    public class Axe : Weapon
    {
        public AxeVariant Variant { get; }

        protected Axe(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, AxeVariant variant, InventorySpaceAmount inventorySpaceAmount)
            : base
                  (damageAmount: damageAmount,
                  durability: durability,
                  rarity: rarity,
                  level: level,
                  name: name,
                  weaponType: WeaponType.AXE,
                  element: element,
                  inventorySpaceAmount: inventorySpaceAmount)
        {
            Variant = variant;
        }
    }
}
