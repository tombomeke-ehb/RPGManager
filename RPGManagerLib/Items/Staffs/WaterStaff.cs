using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Staffs
{
    internal class WaterStaff : Staff
    {
        public WaterStaff()
            : base(damageAmount: 3,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Water Staff",
                  element: Element.WATER,
                  variant: StaffVariant.WATER)
        { }
    }
}
