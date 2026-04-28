namespace RPGManagerLib.Items.Weapons.Bows
{
    /// <summary>
    /// Represents an abstract base class for all bow weapons, providing common properties and behaviors for derived bow
    /// types.
    /// </summary>
    /// <remarks>Derived classes must implement specific bow behaviors and characteristics, such as shooting
    /// mechanics and special abilities. This class inherits from the Weapon class, ensuring that all bows have
    /// essential weapon attributes like damage, durability, and rarity.</remarks>
    public abstract class Bow : Weapon
    {
        public BowVariant Variant { get; }

        /// <summary>
        /// Initializes a new instance of the Bow class with the specified attributes.
        /// </summary>
        /// <param name="damageAmount">The amount of damage the bow inflicts on targets. Must be a positive integer.</param>
        /// <param name="durability">The number of uses the bow can withstand before breaking. Must be a positive integer.</param>
        /// <param name="rarity">The rarity level of the bow, which may affect its value and performance.</param>
        /// <param name="level">The required character level to equip or use the bow.</param>
        /// <param name="name">The name of the bow, used for identification and display purposes.</param>
        /// <param name="variant">The concrete bow variant.</param>
        /// <param name="element">The elemental attribute of the bow, which may influence its damage type or effects.</param>
        /// <param name="inventorySpaceAmount">The amount of inventory space required to store the bow.</param>
        public Bow(int damageAmount, int durability, Rarity rarity, int level, string name, BowVariant variant, Element element, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, 
                  durability, 
                  rarity, 
                  level, 
                  name, 
                  WeaponType.BOW, 
                  element, 
                  inventorySpaceAmount)
        {
            Variant = variant;
        }
    }
}