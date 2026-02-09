using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Weapons.Melee
{
    /// <summary>
    /// A fast melee weapon with low damage and short cooldown.
    /// </summary>
    public class Dagger : Weapon
    {
        /// <summary>
        /// Initializes a new <see cref="Dagger"/> with default values.
        /// </summary>
        public Dagger()
            : base(damageAmount: 8,
                  durability: 50,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Simple Dagger",
                  weaponType: WeaponType.DAGGER,
                  element: Element.NONE,
                  inventorySpaceAmount: InventorySpaceAmount.SMALL
            )
        { }
    }
}
