using RPGManagerLib.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Staffs
{
    internal class FireStaff : Staff
    {
        public FireStaff()
            : base(damageAmount: 3,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Fire Staff",
                  element: Element.FIRE,
                  variant: StaffVariant.FIRE)
        { }
    }
}
