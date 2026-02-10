using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Represents the data required to save and restore the state of a weapon.
    /// </summary>
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
        /// Initializes a new instance of the <see cref="WeaponSaveData"/> class using the specified weapon.
        /// </summary>
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
        /// Converts the current object into a specific <see cref="Weapon"/> instance.
        /// </summary>
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