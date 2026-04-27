using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items.Staffs;
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
        /// Bow variant used when <see cref="WeaponType"/> is <see cref="Items.Weapons.WeaponType.BOW"/>.
        /// </summary>
        public BowVariant? BowVariant { get; set; }

        /// <summary>
        /// Staff variant used when <see cref="WeaponType"/> is <see cref="Items.Weapons.WeaponType.STAFF"/>.
        /// </summary>
        public StaffVariant? StaffVariant { get; set; }

        /// <summary>
        /// Sword variant used when <see cref="WeaponType"/> is <see cref="Items.Weapons.WeaponType.SWORD"/>.
        /// </summary>
        public SwordVariant? SwordVariant { get; set; }

        /// <summary>
        /// Axe variant used when <see cref="WeaponType"/> is <see cref="Items.Weapons.WeaponType.AXE"/>.
        /// </summary>
        public AxeVariant? AxeVariant { get; set; }

        /// <summary>
        /// Dagger variant used when <see cref="WeaponType"/> is <see cref="Items.Weapons.WeaponType.DAGGER"/>.
        /// </summary>
        public DaggerVariant? DaggerVariant { get; set; }

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
            BowVariant = weapon is Bow bow ? bow.Variant : null;
            StaffVariant = weapon is Staff staff ? staff.Variant : null;
            SwordVariant = weapon is Sword sword ? sword.Variant : null;
            AxeVariant = weapon is Axe axe ? axe.Variant : null;
            DaggerVariant = weapon is Dagger dagger ? dagger.Variant : null;
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
                WeaponType.SWORD => SwordVariant switch
                {
                    Items.Weapons.SwordVariant.GREAT => new GreatSword(),
                    Items.Weapons.SwordVariant.BROAD => new BroadSword(),
                    Items.Weapons.SwordVariant.BASIC => new Sword(),
                    null => new Sword(),
                    _ => throw new Exception($"Unknown sword variant: {SwordVariant}")
                },
                WeaponType.STAFF => StaffVariant switch
                {
                    Items.Weapons.StaffVariant.BASIC => new BasicStaff(),
                    Items.Weapons.StaffVariant.WIND => new WindStaff(),
                    null => new BasicStaff(),
                    _ => throw new Exception($"Unknown staff variant: {StaffVariant}")
                },
                WeaponType.AXE => AxeVariant switch
                {
                    Items.Weapons.AxeVariant.BATTLE => new BattleAxe(),
                    Items.Weapons.AxeVariant.GREAT => new GreatAxe(),
                    Items.Weapons.AxeVariant.BASIC => new Axe(),
                    null => new Axe(),
                    _ => throw new Exception($"Unknown axe variant: {AxeVariant}")
                },
                WeaponType.SPEAR => new Spear(),
                WeaponType.DAGGER => DaggerVariant switch
                {
                    Items.Weapons.DaggerVariant.BASIC => new Dagger(),
                    null => new Dagger(),
                    _ => throw new Exception($"Unknown dagger variant: {DaggerVariant}")
                },
                WeaponType.BOW => BowVariant switch
                {
                    Items.Weapons.BowVariant.SIMPLE => new SimpleBow(),
                    Items.Weapons.BowVariant.HUNTING => new HuntingBow(),
                    Items.Weapons.BowVariant.WAR => new WarBow(),
                    null => new SimpleBow(),
                    _ => throw new Exception($"Unknown bow variant: {BowVariant}")
                },
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