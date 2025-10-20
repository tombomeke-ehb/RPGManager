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
        /// Returns damage adjusted by the rarity multiplier.
        /// </summary>
        public int GetEffectiveDamage() => (int)(DamageAmount * GetRarityMultiplier());

        /// <summary>
        /// Returns durability adjusted by the rarity multiplier.
        /// </summary>
        public int GetEffectiveDurability() => (int)(Durability * GetRarityMultiplier());

        /// <summary>
        /// Promotes the weapon to the next rarity tier if possible.
        /// </summary>
        /// <returns>The updated <see cref="Rarity"/> after the upgrade.</returns>
        public Rarity UpgradeWeapon() { 
            Rarity = Rarity switch
            {
                Rarity.COMMON => Rarity.UNCOMMON,
                Rarity.UNCOMMON => Rarity.RARE,
                Rarity.RARE => Rarity.EPIC,
                Rarity.EPIC => Rarity.LEGENDARY,
                _ => Rarity
            };
            return Rarity;
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

