using RPGManagerLib.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Items.Staffs
{
    public class Staff : IEquipable
    {
        public string Name { get; set; }

        public Rarity Rarity { get; set; }

        public InventorySpaceAmount InventorySpaceAmount => InventorySpaceAmount.LARGE;

        public EquipableType EquipableType => EquipableType.STAFF;

        public MagicType MagicType { get; set; }

        public Staff(string name, Rarity rarity, MagicType magicType)
        {
            Name = name;
            Rarity = rarity;
            MagicType = magicType;
        }
    }
}
