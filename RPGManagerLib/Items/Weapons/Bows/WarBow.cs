using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Weapons.Bows
{
    internal class WarBow : Bow
    {
        public WarBow()
            : base(
                  damageAmount: 25,
                  durability: 160,
                  rarity: Rarity.RARE,
                  level: 10,
                  name: "War Bow",
                  weaponType: WeaponType.WARBOW,
                  element: Element.NONE,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE) { }
    }
}
