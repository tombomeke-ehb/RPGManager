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

            // Map Element -> MagicType by name where possible, fail-safe if not mappable
            if (staff != null && Enum.TryParse<MagicType>(staff.Element.ToString(), out var mappedMagic) && mappedMagic == this.MagicType)
            {
                damage *= 1.25;
            }

            return damage;
        }
    }

}
