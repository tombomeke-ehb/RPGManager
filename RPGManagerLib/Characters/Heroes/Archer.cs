using RPGManagerLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManagerLib.Characters.Heroes
{
    public class Archer : Character
    {
        public List<IEquipable> Weapons { get; set; } = new List<IEquipable>();

        public override string CharacterType => "Archer";

        public Archer() : base() { }

        public Archer(string name): base(name) { }

        public override string ToString()
        {
            string baseInfo = base.ToString();

            string weaponList = Weapons != null && Weapons.Any()
                ? string.Join(", ", Weapons.Select(w => w.Name))
                : "none";

            return $"{baseInfo}\nWeapons: {weaponList}";
        }
    }
}
