using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Weapons.Melee.Daggers
{
    internal class BasicDagger : Dagger
    {
        /// <summary>
        /// Initializes a new <see cref="Dagger"/> with default values.
        /// </summary>
        public BasicDagger()
            : base(
                  damageAmount: 8,
                  durability: 50,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Dagger",
                  element: Element.NONE,
                  variant: DaggerVariant.BASIC,
                  inventorySpaceAmount: InventorySpaceAmount.SMALL)
        {
        }
    }
}
