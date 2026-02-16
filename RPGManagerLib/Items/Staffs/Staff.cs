using RPGManagerLib.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Staffs
{
    public class Staff : Weapon
    {


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
