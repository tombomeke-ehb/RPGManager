using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items;

namespace RPGManagerLib.Spells
{
    public abstract class Spell
    {
        public string Name { get; set; }
        public MagicType MagicType { get; set; }
        public double BaseDamage { get; set; }
        public double ManaCost { get; set; }

        protected Spell(string name, MagicType type, double damage, double manaCost)
        {
            Name = name;
            MagicType = type;
            BaseDamage = damage;
            ManaCost = manaCost;
        }

        public virtual void Cast(Mage caster, Character target)
        {
            if (caster.Mana < ManaCost)
            {
                Console.WriteLine("Not enough mana!");
                return;
            }

            caster.Mana -= ManaCost;

            double finalDamage = CalculateDamage(caster);
            target.Damage(finalDamage);

            Console.WriteLine($"{caster.Name} casts {Name} dealing {finalDamage} damage.");
        }

        protected virtual double CalculateDamage(Mage caster)
        {
            double damage = BaseDamage;

            var staff = caster.Weapons
                .OfType<Staff>()
                .FirstOrDefault();

            if (staff != null && staff.Element == this.MagicType)
            {
                damage *= 1.25;
            }

            return damage;
        }
    }

}
