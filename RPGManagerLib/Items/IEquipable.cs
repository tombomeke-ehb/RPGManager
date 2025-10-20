using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items
{
    /// <summary>
    /// Represents an item that can be equipped by a character or entity in the game.
    /// </summary>
    /// <remarks>This interface defines the common properties of equipable items, such as their name, rarity, 
    /// inventory space requirements, and type. Implementations of this interface can represent  various types of
    /// equipment, such as weapons, armor, or accessories.</remarks>
    public interface IEquipable
    {
        /// <summary>
        /// Display name of the item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Rarity tier of the item.
        /// </summary>
        Rarity Rarity { get; }

        /// <summary>
        /// Inventory footprint required by this item.
        /// </summary>
        InventorySpaceAmount InventorySpaceAmount { get; }

        /// <summary>
        /// The broader category of the equipable item.
        /// </summary>
        EquipableType EquipableType { get; }
    }
}
