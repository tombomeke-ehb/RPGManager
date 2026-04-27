using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Weapons.Melee
{
    /// <summary>
    /// A fast melee weapon with low damage and short cooldown.
    /// </summary>
    public class Dagger : Weapon
    {
        public DaggerVariant Variant { get; }

        /// <summary>
        /// Initializes a new <see cref="Dagger"/> with default values.
        /// </summary>
        public Dagger()
            : this(
                  damageAmount: 8,
                  durability: 50,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Dagger",
                  element: Element.NONE,
                  variant: DaggerVariant.BASIC,
                  inventorySpaceAmount: InventorySpaceAmount.SMALL)
        {
        }

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
