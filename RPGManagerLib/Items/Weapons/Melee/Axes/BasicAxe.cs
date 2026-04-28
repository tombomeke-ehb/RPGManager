using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Weapons.Melee.Axes
{
    internal class BasicAxe : Axe
    {
        /// <summary>
        /// Initializes a new instance of the Axe class with predefined attributes for a basic axe weapon.
        /// </summary>
        /// <remarks>This constructor creates an axe with a damage amount of 20, durability of 90, common
        /// rarity, level 1, and no elemental properties. The axe is named "Basic Axe" and is categorized as an axe-type
        /// weapon that occupies a large amount of inventory space.</remarks>
        public BasicAxe()
            : base (
                  damageAmount: 20,
                  durability: 90,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Axe",
                  element: Element.NONE,
                  variant: AxeVariant.BASIC,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
        }
    }
}
