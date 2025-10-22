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
                  cooldownTime: 1.5, 
                  inventorySpaceAmount: InventorySpaceAmount.SMALL)
        { }

        /// <summary>
        /// Initializes a new <see cref="Sword"/> with explicit properties.
        /// </summary>
        public Sword(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, double coolDownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.SWORD, element, coolDownTime, inventorySpaceAmount)
        { }
    }
}
