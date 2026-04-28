using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Staffs
{
    /// <summary>
    /// Represents a staff weapon that can be used in combat, providing basic damage and durability attributes.
    /// </summary>
    /// <remarks>The Staff class inherits from the Weapon class and is designed for use in various combat
    /// scenarios. It can be customized with different durability, rarity, level, name, and elemental
    /// attributes.</remarks>
    public abstract class Staff : Weapon
    {

        public StaffVariant Variant { get; }

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
        public Staff(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, StaffVariant variant)
            : base(damageAmount: damageAmount,
                  durability: durability,
                  rarity: rarity,
                  level: level,
                  name: name,
                  weaponType: WeaponType.STAFF,
                  element: element,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
            Variant = variant;
        }
    }
}
