using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Weapons.Melee
{
    /// <summary>
    /// A heavy melee weapon with strong base damage and slower cooldown.
    /// </summary>
    public class Axe : Weapon
    {

        /// <summary>
        /// Initializes a new <see cref="Axe"/> with default values.
        /// </summary>
        public Axe()
            : base
                  (damageAmount: 20,
                  durability: 90,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Axe",
                  weaponType: WeaponType.AXE,
                  element: Element.NONE,
                  cooldownTime: 2.5,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="Axe"/> with explicit properties.
        /// </summary>
        public Axe(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, double coolDownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.AXE, element, coolDownTime, inventorySpaceAmount)
        {

        }
    }
}
