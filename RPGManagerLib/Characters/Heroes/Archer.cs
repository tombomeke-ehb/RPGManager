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
        public List<IEquipable> Weapons {  get; set; }

        public override string CharacterType => "Archer";

        public Archer() : base()
        {
            Weapons = new List<IEquipable>();
        }

        public Archer(string name, List<IEquipable> weapons, int gold)
            : base(name, gold)
        {
            Weapons = weapons ?? new List<IEquipable>();
        }

        public Archer(string name, double health, DateTime creationDate, int powerLevel, List<IEquipable> weapons, int gold)
            : base(name, health, creationDate, powerLevel, gold)
        {
            Weapons = weapons ?? new List<IEquipable>();
        }

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
