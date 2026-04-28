using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Weapons.Melee.Swords
{
    internal class BasicSword : Sword
    {
        /// <summary>
        /// Initializes a new <see cref="Sword"/> with default values.
        /// </summary>
        public BasicSword()
            : base(
                  damageAmount: 13,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Sword",
                  element: Element.NONE,
                  variant: SwordVariant.BASIC,
                  inventorySpaceAmount: InventorySpaceAmount.SMALL)
        {
        }
    }
}
