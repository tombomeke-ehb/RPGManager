using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Staffs
{
    /// <summary>
    /// Represents a staff weapon that can be used in combat, providing basic damage and durability attributes.
    /// </summary>
    /// <remarks>The Staff class inherits from the Weapon class and is designed for use in various combat
    /// scenarios. It can be customized with different durability, rarity, level, name, and elemental
    /// attributes.</remarks>
    public class BasicStaff : Staff
    {
        /// <summary>
        /// Initializes a new instance of the Staff class representing a basic staff weapon with predefined attributes
        /// suitable for entry-level gameplay.
        /// </summary>
        /// <remarks>This constructor creates a staff with a damage amount of 3, durability of 100, common
        /// rarity, level 1, the name "Basic Staff", weapon type set to staff, no elemental affinity, and a large
        /// inventory space requirement. Use this constructor to add a standard staff to a player's inventory or as a
        /// default weapon option.</remarks>
        public BasicStaff()
            : base(damageAmount: 3,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Staff",
                  element: Element.NONE,
                  variant: StaffVariant.BASIC)
        { }

    }
}
