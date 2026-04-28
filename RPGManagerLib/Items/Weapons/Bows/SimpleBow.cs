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
                  variant: BowVariant.SIMPLE,
                  element: Element.NONE,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE) { }
    }
}
