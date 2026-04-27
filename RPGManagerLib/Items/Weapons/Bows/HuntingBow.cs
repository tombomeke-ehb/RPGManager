using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Weapons.Bows
{
    internal class HuntingBow : Bow
    {
        public HuntingBow()
            : base(
                  damageAmount: 15,
                  durability: 130,
                  rarity: Rarity.UNCOMMON,
                  level: 5,
                  name: "Hunting Bow",
                  variant: BowVariant.HUNTING,
                  element: Element.NONE,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE) { }
    }
}
