namespace RPGManagerLib.Items.Weapons.Melee.Spears
{
    /// <summary>
    /// A reach melee weapon with solid durability and moderate cooldown.
    /// </summary>
    public class Spear : Weapon
    {
        /// <summary>
        /// Specific variant/type of the spear, which may affect its stats or appearance.
        /// </summary>
        public SpearVariant Variant { get; set; }

        /// <summary>
        /// Initializes a new <see cref="Spear"/> with specified values.
        /// </summary>
        /// <param name="damageAmount"></param>
        /// <param name="durability"></param>
        /// <param name="rarity"></param>
        /// <param name="level"></param>
        /// <param name="name"></param>
        /// <param name="element"></param>
        /// <param name="variant"></param>
        /// <param name="inventorySpaceAmount"></param>

        public Spear(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, SpearVariant variant, InventorySpaceAmount inventorySpaceAmount)
            : base
                  (damageAmount: damageAmount,
                  durability: durability,
                  rarity: rarity,
                  level: level,
                  name: name,
                  weaponType: WeaponType.SPEAR,
                  element: element,
                  inventorySpaceAmount: inventorySpaceAmount)
        {
            Variant = variant;
        }
    }
