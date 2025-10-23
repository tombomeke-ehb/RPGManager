using RPGManagerLib.Exceptions;
using RPGManagerLib.Items;

namespace RPGManagerLib.Items.Weapons
{
    /// <summary>
    /// Base type for all weapons that can be equipped by a character.
    /// </summary>
    /// <remarks>
    /// Provides common weapon properties and helpers, such as rarity scaling
    /// and effective damage/durability calculations.
    /// </remarks>
    public abstract class Weapon : IEquipable
    {
        /// <summary>
        /// Raw damage value before modifiers.
        /// </summary>
        public int DamageAmount { get; set; }

        /// <summary>
        /// Raw durability value before modifiers.
        /// </summary>
        public int Durability { get; set; }

        /// <summary>
        /// Rarity tier of the weapon (affects scaling).
        /// </summary>
        public Rarity Rarity { get; set; }

        /// <summary>
        /// Weapon level (game-specific usage).
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Display name of the weapon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Concrete weapon kind (e.g., <see cref="WeaponType.SWORD"/>).
        /// </summary>
        public WeaponType Type { get; set; }

        /// <summary>
        /// Optional elemental affinity for the weapon.
        /// </summary>
        public Element Element { get; set; } = Element.NONE;

        /// <summary>
        /// Cooldown time in seconds between actions.
        /// </summary>
        public double CooldownTime { get; set; }

        /// <summary>
        /// How many inventory slots this weapon occupies.
        /// </summary>
        public InventorySpaceAmount InventorySpaceAmount { get; set; }

        /// <summary>
        /// The item category, always <see cref="EquipableType.WEAPON"/> for weapons.
        /// </summary>
        public EquipableType EquipableType => EquipableType.WEAPON;

        /// <summary>
        /// Initializes a new weapon with explicit properties.
        /// </summary>
        /// <param name="damageAmount">Base damage output.</param>
        /// <param name="durability">Base durability value.</param>
        /// <param name="rarity">Rarity tier.</param>
        /// <param name="level">Weapon level.</param>
        /// <param name="name">Display name.</param>
        /// <param name="weaponType">Concrete weapon kind.</param>
        /// <param name="element">Elemental attribute.</param>
        /// <param name="cooldownTime">Cooldown time (seconds).</param>
        /// <param name="inventorySpaceAmount">Inventory footprint.</param>
        public Weapon(int damageAmount, int durability, Rarity rarity, int level, string name, WeaponType weaponType, Element element,double cooldownTime, InventorySpaceAmount inventorySpaceAmount)
        {
            DamageAmount = damageAmount;
            Durability = durability;
            Rarity = rarity;
            Level = level;
            Name = name;
            Type = weaponType;
            Element = element;
            CooldownTime = cooldownTime;
            InventorySpaceAmount = inventorySpaceAmount;
        }

        /// <summary>
        /// Returns the multiplicative factor contributed by the current <see cref="Rarity"/>.
        /// </summary>
        public double GetRarityMultiplier() =>
            Rarity switch
            {
                Rarity.COMMON => 1.0,
                Rarity.UNCOMMON => 1.2,
                Rarity.RARE => 1.5,
                Rarity.EPIC => 2.0,
                Rarity.LEGENDARY => 3.0,
                _ => 1.0
            };

        /// <summary>
        /// Static helper to get the rarity multiplier for a given tier.
        /// </summary>
        public static double GetMultiplierForRarity(Rarity rarity) => rarity switch
        {
            Rarity.COMMON => 1.0,
            Rarity.UNCOMMON => 1.2,
            Rarity.RARE => 1.5,
            Rarity.EPIC => 2.0,
            Rarity.LEGENDARY => 3.0,
            _ => 1.0
        };

        /// <summary>
        /// Returns damage adjusted by the rarity multiplier.
        /// </summary>
        public int GetEffectiveDamage() => (int)(DamageAmount * GetRarityMultiplier());

        /// <summary>
        /// Returns durability adjusted by the rarity multiplier.
        /// </summary>
        public int GetEffectiveDurability() => (int)(Durability * GetRarityMultiplier());

        /// <summary>
        /// Returns the minimum rarity implied by the current <see cref="Level"/>.
        /// </summary>
        /// <remarks>
        /// This models a progression where item level gradually unlocks higher rarity tiers over time.
        /// Finding a high-rarity item at a low level is valid; we never downgrade rarity.
        /// Example thresholds (tweak as desired):
        /// 0-4: Common, 5-9: Uncommon, 10-14: Rare, 15-19: Epic, 20+: Legendary.
        /// </remarks>
        public Rarity GetTargetRarityForLevel()
        {
            if (Level >= 20) return Rarity.LEGENDARY;
            if (Level >= 15) return Rarity.EPIC;
            if (Level >= 10) return Rarity.RARE;
            if (Level >= 5) return Rarity.UNCOMMON;
            return Rarity.COMMON;
        }

        /// <summary>
        /// Ensures <see cref="Rarity"/> is at least the rarity implied by <see cref="Level"/>.
        /// Does not downgrade; returns true if rarity changed.
        /// </summary>
        public bool SyncRarityWithLevel()
        {
            var target = GetTargetRarityForLevel();
            if (target > Rarity)
            {
                Rarity = target;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the minimum level required for a specific rarity tier.
        /// </summary>
        public static int GetMinLevelForRarity(Rarity rarity) => rarity switch
        {
            Rarity.COMMON => 0,
            Rarity.UNCOMMON => 5,
            Rarity.RARE => 10,
            Rarity.EPIC => 15,
            Rarity.LEGENDARY => 20,
            _ => 0
        };

        /// <summary>
        /// Returns the next level threshold at which rarity would increase, or null if at max tier.
        /// </summary>
        public int? GetNextRarityThresholdLevel()
        {
            if (Level < 5) return 5;
            if (Level < 10) return 10;
            if (Level < 15) return 15;
            if (Level < 20) return 20;
            return null; // already at or above legendary threshold
        }
    }

    /// <summary>
    /// Rarity tiers for equipable items.
    /// </summary>
    public enum Rarity { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }

    /// <summary>
    /// Inventory footprint sizes.
    /// </summary>
    public enum InventorySpaceAmount { SMALL, LARGE }

    /// <summary>
    /// Supported concrete weapon kinds.
    /// </summary>
    public enum WeaponType { SWORD, STAFF, AXE, SPEAR, DAGGER, SIMPLEBOW }

    /// <summary>
    /// Optional elemental affinities attachable to weapons.
    /// </summary>
    public enum Element { NONE, FIRE, ICE, LIGHTNING, POISON }
}

