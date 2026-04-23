using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Represents the data required to serialize and reconstruct a weapon, including its type, damage, durability,
    /// level, and elemental affinity.
    /// </summary>
    /// <remarks>This class inherits from <see cref="EquipableSaveData"/> and is used to store weapon-specific
    /// information for saving and loading purposes. It provides methods to convert the saved data back into a <see
    /// cref="Weapon"/> instance, enabling persistence and restoration of weapon state within the game.</remarks>
    public class WeaponSaveData : EquipableSaveData
    {
        /// <summary>
        /// The concrete weapon kind to reconstruct.
        /// </summary>
        public WeaponType WeaponType { get; set; }

        /// <summary>
        /// Base damage amount.
        /// </summary>
        public int DamageAmount { get; set; }

        /// <summary>
        /// Base durability value.
        /// </summary>
        public int Durability { get; set; }

        /// <summary>
        /// Weapon level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Elemental affinity.
        /// </summary>
        public Element Element { get; set; }

        public WeaponSaveData() { }

        /// <summary>
        /// Initializes a new instance of the WeaponSaveData class using the specified weapon object.
        /// </summary>
        /// <remarks>This constructor copies relevant properties from the provided weapon to create a save
        /// data representation. Use this when persisting weapon information for serialization or storage.</remarks>
        /// <param name="weapon">The weapon object whose properties are used to populate the WeaponSaveData instance. Cannot be null.</param>
        public WeaponSaveData(Weapon weapon)
        {
            // Base class properties
            Name = weapon.Name;
            Rarity = weapon.Rarity;
            InventorySpaceAmount = weapon.InventorySpaceAmount;

            // Specific properties
            WeaponType = weapon.Type;
            DamageAmount = weapon.DamageAmount;
            Durability = weapon.Durability;
            Level = weapon.Level;
            Element = weapon.Element;
        }

        /// <summary>
        /// Creates an instance of the appropriate weapon type based on the current object's WeaponType and initializes
        /// it with the object's properties.
        /// </summary>
        /// <remarks>This method supports various weapon types, including sword, axe, spear, dagger, and
        /// simple bow. Ensure that the WeaponType is set to a valid value before calling this method.</remarks>
        /// <returns>An instance of the IEquipable interface representing the created weapon, populated with the current object's
        /// data.</returns>
        /// <exception cref="Exception">Thrown if the WeaponType is unknown, indicating that the specified weapon type cannot be instantiated.</exception>
        public override IEquipable ToEquipable()
        {
            // 1. Create the specific weapon instance with default values
            Weapon w = WeaponType switch
            {
                WeaponType.SWORD => new Sword(),
                WeaponType.AXE => new Axe(),
                WeaponType.SPEAR => new Spear(),
                WeaponType.DAGGER => new Dagger(),
                WeaponType.SIMPLEBOW => new SimpleBow(),
                WeaponType.HUNTINGBOW => new HuntingBow(),
                WeaponType.WARBOW => new WarBow(),
                _ => throw new Exception($"Unknown weapon type: {WeaponType}")
            };

            // 2. Overwrite with saved data
            w.Name = Name;
            w.Rarity = Rarity;
            w.InventorySpaceAmount = InventorySpaceAmount;

            w.Durability = Durability;
            w.DamageAmount = DamageAmount;
            w.Level = Level;
            w.Element = Element;

            return w;
        }
    }
}