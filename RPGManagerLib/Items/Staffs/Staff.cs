using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Staffs
{
    /// <summary>
    /// Represents a staff weapon that can be used in combat, providing basic damage and durability attributes.
    /// </summary>
    /// <remarks>The Staff class inherits from the Weapon class and is designed for use in various combat
    /// scenarios. It can be customized with different durability, rarity, level, name, and elemental
    /// attributes.</remarks>
    public class Staff : Weapon
    {
        /// <summary>
        /// Initializes a new instance of the Staff class representing a basic staff weapon with predefined attributes
        /// suitable for entry-level gameplay.
        /// </summary>
        /// <remarks>This constructor creates a staff with a damage amount of 3, durability of 100, common
        /// rarity, level 1, the name "Basic Staff", weapon type set to staff, no elemental affinity, and a large
        /// inventory space requirement. Use this constructor to add a standard staff to a player's inventory or as a
        /// default weapon option.</remarks>
        public Staff()
            : base(damageAmount: 3,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Staff",
                  weaponType: WeaponType.STAFF,
                  element: Element.NONE,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        { }

        /// <summary>
        /// Initializes a new instance of the Staff class with the specified durability, rarity, level, name, and
        /// element.
        /// </summary>
        /// <remarks>This constructor sets the staff's damage amount to 3 and allocates a large inventory
        /// space for the item.</remarks>
        /// <param name="durability">The durability of the staff, which determines how many uses it can withstand before breaking.</param>
        /// <param name="rarity">The rarity of the staff, which affects its power and value.</param>
        /// <param name="level">The level of the staff, indicating its strength and effectiveness.</param>
        /// <param name="name">The name of the staff, used for identification and display.</param>
        /// <param name="element">The elemental type of the staff, which influences its magical properties and effects.</param>
        public Staff(int durability, Rarity rarity, int level, string name, Element element)
            : base(damageAmount: 3,
                  durability: durability,
                  rarity: rarity,
                  level: level,
                  name: name,
                  weaponType: WeaponType.STAFF,
                  element: element,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        { }
    }
}
