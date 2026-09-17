using System;
using System.Linq;
using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items;
using RPGManagerLib.Items.Staffs;

namespace RPGManagerLib.Spells
{
    public abstract class Spell
    {
        // TODO: Move spell metadata toward unlockable/progression-aware content once leveling and spell acquisition are added.
        public string Name { get; set; }
        public Element Element { get; set; }
        public double BaseDamage { get; set; }
        public double ManaCost { get; set; }

        protected Spell(string name, Element element, double damage, double manaCost)
        {
            Name = name;
            Element = element;
            BaseDamage = damage;
            ManaCost = manaCost;
        }

        public virtual void Cast(Mage caster, Character target)
        {
            // TODO: Integrate combat logging, target validation, and status-effect resolution with CombatManager instead of direct console-only flow.
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

            if (staff != null && staff.Element == this.Element)
            {
                damage *= 1.25;
            }

            return damage;
        }
    }

}
