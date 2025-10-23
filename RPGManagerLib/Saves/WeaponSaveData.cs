using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Represents the data required to save and restore the state of a weapon.
    /// </summary>
    /// <remarks>This class is used to serialize and deserialize weapon data for persistence purposes. It encapsulates
    /// all the properties necessary to recreate a weapon, including its type, name,  damage, durability, rarity, level,
    /// elemental attributes, cooldown time, and inventory space requirements.</remarks>
    public class WeaponSaveData
    {
        /// <summary>
        /// The concrete weapon kind to reconstruct.
        /// </summary>
        public WeaponType WeaponType { get; set; }

        /// <summary>
        /// Display name of the weapon.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Base damage amount.
        /// </summary>
        public int DamageAmount { get; set; }

        /// <summary>
        /// Base durability value.
        /// </summary>
        public int Durability { get; set; }

        /// <summary>
        /// Rarity tier of the weapon.
        /// </summary>
        public Rarity Rarity { get; set; }

        /// <summary>
        /// Weapon level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Baseline damage before level scaling.
        /// </summary>
        public int BaseDamage { get; set; }

        /// <summary>
        /// Baseline durability before rarity scaling.
        /// </summary>
        public int BaseDurability { get; set; }

        /// <summary>
        /// Elemental affinity.
        /// </summary>
        public Element Element { get; set; }

        /// <summary>
        /// Cooldown time between actions (seconds).
        /// </summary>
        public double CoolDownTime { get; set; }

        /// <summary>
        /// Inventory footprint size.
        /// </summary>
        public InventorySpaceAmount InventorySpaceAmount { get; set; }

        public WeaponSaveData() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponSaveData"/> class using the specified weapon.
        /// </summary>
        /// <remarks>This constructor extracts the relevant properties from the provided <paramref name="weapon"/>
        /// and initializes the save data fields accordingly. Ensure that the <paramref name="weapon"/>  instance is fully
        /// populated before passing it to this constructor.</remarks>
        /// <param name="weapon">The weapon from which to populate the save data. Cannot be <see langword="null"/>.</param>
        public WeaponSaveData(Weapon weapon)
        {
            WeaponType = weapon.Type;
            Name = weapon.Name;
            DamageAmount = weapon.DamageAmount;
            Durability = weapon.Durability;
            Rarity = weapon.Rarity;
            Level = weapon.Level;
            Element = weapon.Element;
            CoolDownTime = weapon.CooldownTime;
            InventorySpaceAmount = weapon.InventorySpaceAmount;

            // Derive baselines so we can reconstruct consistently on load
            BaseDamage = (int)Math.Round(weapon.DamageAmount / Math.Pow(1.10, Math.Max(0, weapon.Level)));
            BaseDurability = (int)Math.Round(weapon.Durability / weapon.GetRarityMultiplier());
        }

        /// <summary>
        /// Converts the current object into a specific <see cref="Weapon"/> instance based on the <see cref="WeaponType"/>
        /// property.
        /// </summary>
        /// <remarks>The method creates a new instance of a weapon subclass (e.g., <see cref="Sword"/>, <see
        /// cref="Axe"/>, <see cref="Spear"/>, or <see cref="SimpleBow"/>) based on the value of the <see
        /// cref="WeaponType"/> property. Each weapon is initialized with the relevant properties of the current
        /// object.</remarks>
        /// <returns>A <see cref="Weapon"/> instance corresponding to the specified <see cref="WeaponType"/>.</returns>
        /// <exception cref="Exception">Thrown if the <see cref="WeaponType"/> property contains an unknown or unsupported value.</exception>
        public Weapon ToWeapon()
        {
            // Fall back if older saves don't have baselines
            int baseDmg = BaseDamage > 0 ? BaseDamage : (int)Math.Round(DamageAmount / Math.Pow(1.10, Math.Max(0, Level)));
            double savedMultiplier = Weapon.GetMultiplierForRarity(Rarity);
            int baseDur = BaseDurability > 0 ? BaseDurability : (int)Math.Round(Durability / savedMultiplier);

            Weapon w = WeaponType switch
            {
                WeaponType.SWORD => new Sword(
                    DamageAmount,
                    Durability,
                    Rarity,
                    Level,
                    Name,
                    Element,
                    CoolDownTime,
                    InventorySpaceAmount
                ),
                WeaponType.AXE => new Axe(
                    DamageAmount,
                    Durability,
                    Rarity,
                    Level,
                    Name,
                    Element,
                    CoolDownTime,
                    InventorySpaceAmount
                ),
                WeaponType.SPEAR => new Spear(
                    DamageAmount,
                    Durability,
                    Rarity,
                    Level,
                    Name,
                    Element,
                    CoolDownTime,
                    InventorySpaceAmount
                ),
                WeaponType.DAGGER => new Dagger(
                    DamageAmount,
                    Durability,
                    Rarity,
                    Level,
                    Name,
                    Element,
                    CoolDownTime,
                    InventorySpaceAmount
                ),
                WeaponType.SIMPLEBOW => new SimpleBow(
                    DamageAmount,
                    Durability,
                    Rarity,
                    Level,
                    Name,
                    Element,
                    CoolDownTime,
                    InventorySpaceAmount
                ),
                _ => throw new Exception($"Unknown weapon type: {WeaponType}")
            };

            // Now recalc consistency using the constructed weapon's own multiplier
            // Ensure rarity is at least what level implies, but never downgrade saved rarity
            var savedRarity = w.Rarity;
            w.SyncRarityWithLevel();
            if (savedRarity > w.Rarity) w.Rarity = savedRarity;

            // Re-derive stats from baselines (failsafe)
            w.DamageAmount = (int)Math.Round(baseDmg * Math.Pow(1.10, Math.Max(0, w.Level)));
            w.Durability = (int)Math.Round(baseDur * w.GetRarityMultiplier());

            return w;
        }
    }
}
