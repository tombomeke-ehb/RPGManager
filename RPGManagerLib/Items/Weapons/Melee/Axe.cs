using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Weapons.Melee
{
    /// <summary>
    /// Represents a melee weapon of type axe with predefined damage, durability, rarity, and inventory space
    /// attributes.
    /// </summary>
    /// <remarks>The <see cref="Axe"/> class inherits from <see cref="Weapon"/> and is initialized with
    /// default values suitable for entry-level gameplay. It is categorized as a common item, deals moderate damage, and
    /// occupies a large inventory space. This class is intended for use by players at level 1 and does not possess
    /// elemental properties.</remarks>
    public class Axe : Weapon
    {

        /// <summary>
        /// Initializes a new instance of the Axe class with predefined attributes for a basic axe weapon.
        /// </summary>
        /// <remarks>This constructor creates an axe with a damage amount of 20, durability of 90, common
        /// rarity, level 1, and no elemental properties. The axe is named "Basic Axe" and is categorized as an axe-type
        /// weapon that occupies a large amount of inventory space.</remarks>
        public Axe()
            : base
                  (damageAmount: 20,
                  durability: 90,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Axe",
                  weaponType: WeaponType.AXE,
                  element: Element.NONE,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
        }
    }
}
