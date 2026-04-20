using System;
using System.Linq;
using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items;
using RPGManagerLib.Items.Staffs;

namespace RPGManagerLib.Spells
{
    /// <summary>
    /// Abstract base class representing a spell that can be cast by a mage. It defines common properties such as name, magic type,
    /// base damage, and mana cost.
    /// </summary>
    /// <remarks>Derived classes must implement specific behavior and characteristics</remarks>
    public abstract class Spell
    {
        /// <summary>
        /// Name of the spell
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Type of magic the spell is
        /// </summary>
        public MagicType MagicType { get; set; }
        /// <summary>
        /// Amount of damage that the spell does
        /// </summary>
        public double BaseDamage { get; set; }
        /// <summary>
        /// Amount of Mana that it costs to cast the spell
        /// </summary>
        public double ManaCost { get; set; }

        /// <summary>
        /// Initializes a new instance of the Spell class with the specified name, magic type, base damage, and mana cost.
        /// </summary>
        /// <param name="name">The name of the spell.</param>
        /// <param name="type">The type of magic the spell belongs to.</param>
        /// <param name="damage">The base damage of the spell.</param>
        /// <param name="manaCost">The mana cost to cast the spell.</param>
        protected Spell(string name, MagicType type, double damage, double manaCost)
        {
            Name = name;
            MagicType = type;
            BaseDamage = damage;
            ManaCost = manaCost;
        }

        /// <summary>
        /// Casts the spell from the caster to the target. It checks if the caster has enough mana, reduces the caster's mana by the spell's mana cost,
        /// and applies the calculated damage to the target.
        /// </summary>
        /// <param name="caster">The mage casting the spell.</param>
        /// <param name="target">The character receiving the spell's effects.</param>
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

            // Map Element -> MagicType by name where possible, fail-safe if not mappable
            if (staff != null && Enum.TryParse<MagicType>(staff.Element.ToString(), out var mappedMagic) && mappedMagic == this.MagicType)
            {
                damage *= 1.25;
            }

            return damage;
        }
    }

}
