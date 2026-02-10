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
    /// elemental attributes, and inventory space requirements.</remarks>
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
        /// Elemental affinity.
        /// </summary>
        public Element Element { get; set; }

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
            InventorySpaceAmount = weapon.InventorySpaceAmount;
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
            Weapon w = WeaponType switch
            {
                WeaponType.SWORD => new Sword(),
                WeaponType.AXE => new Axe(),
                WeaponType.SPEAR => new Spear(),
                WeaponType.DAGGER => new Dagger(),
                WeaponType.SIMPLEBOW => new SimpleBow(),
                _ => throw new Exception($"Unknown weapon type: {WeaponType}")
            };

            w.Durability = this.Durability;
            w.DamageAmount = this.DamageAmount;
            w.Level = this.Level;
            w.Rarity = this.Rarity;
            w.Element = this.Element;
            w.Name = this.Name;
            w.InventorySpaceAmount = this.InventorySpaceAmount;

            return w;
        }
    }
}

// TODO: Add Mage weapons like staffs, etc.