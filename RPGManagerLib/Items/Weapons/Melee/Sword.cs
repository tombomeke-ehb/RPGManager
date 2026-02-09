using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Weapons.Melee
{
    /// <summary>
    /// A balanced melee weapon with moderate damage and cooldown.
    /// </summary>
    public class Sword : Weapon
    {
        /// <summary>
        /// Initializes a new <see cref="Sword"/> with default values.
        /// </summary>
        public Sword()
            : base
                  (damageAmount: 13, 
                  durability: 100, 
                  rarity: Rarity.COMMON, 
                  level: 1, 
                  name: "Basic Sword", 
                  weaponType: WeaponType.SWORD, 
                  element: Element.NONE, 
                  inventorySpaceAmount: InventorySpaceAmount.SMALL)
        { }
    }
}
