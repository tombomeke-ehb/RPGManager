using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Staffs
{
    internal class IceStaff : Staff
    {
        public IceStaff()
            : base(damageAmount: 3,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Ice Staff",
                  element: Element.ICE,
                  variant: StaffVariant.ICE)
        { }
    }
}
