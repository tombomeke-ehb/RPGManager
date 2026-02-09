using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Weapons.Melee
{
    /// <summary>
    /// A reach melee weapon with solid durability and moderate cooldown.
    /// </summary>
    public class Spear : Weapon
    {
        /// <summary>
        /// Initializes a new <see cref="Spear"/> with default values.
        /// </summary>
        public Spear() 
            : base
                  (damageAmount: 17,
                  durability: 120,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Spear",
                  weaponType: WeaponType.SPEAR,
                  element: Element.NONE,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE) { }
    }
}
