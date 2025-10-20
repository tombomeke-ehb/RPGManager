namespace RPGManagerLib.Items.Weapons.Bows
{
    /// <summary>
    /// A basic bow with common stats suitable for early gameplay.
    /// </summary>
    internal class SimpleBow : Bow
    {
        /// <summary>
        /// Initializes a new <see cref="SimpleBow"/> with default values.
        /// </summary>
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

        /// <summary>
        /// Initializes a new <see cref="SimpleBow"/> with explicit properties.
        /// </summary>
        public SimpleBow(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, double cooldownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.SIMPLEBOW, element, cooldownTime, inventorySpaceAmount)
        {
        }
    }
}
