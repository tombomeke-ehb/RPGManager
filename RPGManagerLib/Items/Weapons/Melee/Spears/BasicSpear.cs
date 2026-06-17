using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Weapons.Melee.Spears
{
    /// <summary>
    /// A basic spear with balanced damage and durability, suitable for close combat.
    /// </summary>
    internal class BasicSpear : Weapon
    {

        /// <summary>
        /// Initializes a new <see cref="BasicSpear"/> with default values.
        /// </summary>
        public BasicSpear()
            : base(damageAmount: 10,
             durability: 100,
             rarity: Rarity.COMMON,
             level: 1,
             name: "Basic Spear",
             weaponType: WeaponType.SPEAR,
             element: Element.NONE,
             inventorySpaceAmount: InventorySpaceAmount.SMALL)
        {

        }
    }
}
