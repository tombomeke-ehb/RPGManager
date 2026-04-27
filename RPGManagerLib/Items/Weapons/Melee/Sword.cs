using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Weapons.Melee
{
    /// <summary>
    /// A balanced melee weapon with moderate damage and cooldown.
    /// </summary>
    public class Sword : Weapon
    {
        public SwordVariant Variant { get; }

        /// <summary>
        /// Initializes a new <see cref="Sword"/> with default values.
        /// </summary>
        public Sword()
            : this(
                  damageAmount: 13,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Short Sword",
                  element: Element.NONE,
                  variant: SwordVariant.BASIC,
                  inventorySpaceAmount: InventorySpaceAmount.SMALL)
        {
        }

        protected Sword(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, SwordVariant variant, InventorySpaceAmount inventorySpaceAmount)
            : base
                  (damageAmount: damageAmount,
                  durability: durability,
                  rarity: rarity,
                  level: level,
                  name: name,
                  weaponType: WeaponType.SWORD,
                  element: element,
                  inventorySpaceAmount: inventorySpaceAmount)
        {
            Variant = variant;
        }
    }
}
