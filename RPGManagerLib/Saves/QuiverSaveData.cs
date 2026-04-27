using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Weapons.Quivers;
using RPGManagerLib.Items;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Represents the save data for a quiver, including its capacity and other equipable properties.
    /// </summary>
    /// <remarks>This class inherits from EquipableSaveData and is used to store the state of a quiver in the
    /// game. It provides functionality to convert the saved data back into an equipable quiver instance.</remarks>
    public class QuiverSaveData : EquipableSaveData
    {
        public int Capacity { get; set; }
        public QuiverVariant? QuiverVariant { get; set; }

        public QuiverSaveData() { }
        /// <summary>
        /// Initializes a new instance of the QuiverSaveData class using the specified Quiver object.
        /// </summary>
        /// <remarks>This constructor copies the base properties such as Name, Rarity, and
        /// InventorySpaceAmount from the provided Quiver object, as well as the specific property Capacity.</remarks>
        /// <param name="quiver">The Quiver object containing the properties to initialize the QuiverSaveData instance. This parameter cannot
        /// be null.</param>
        public QuiverSaveData(Quiver quiver)
        {
            // Base properties
            Name = quiver.Name;
            Rarity = quiver.Rarity;
            InventorySpaceAmount = quiver.InventorySpaceAmount;

            // Specific properties
            Capacity = quiver.Capacity;
            QuiverVariant = quiver.Variant;
        }
        /// <summary>
        /// Creates an equipable quiver instance based on the current object's properties.
        /// </summary>
        /// <remarks>Currently, this method only supports the SmallQuiver type. Additional quiver types
        /// may be supported in the future, which will require updates to this method's logic.</remarks>
        /// <returns>An instance of IEquipable representing the quiver with initialized properties.</returns>
        public override IEquipable ToEquipable()
        {
            Quiver q = QuiverVariant switch
            {
                Weapons.Quivers.QuiverVariant.SMALL => new SmallQuiver(),
                Weapons.Quivers.QuiverVariant.MEDIUM => new MediumQuiver(),
                Weapons.Quivers.QuiverVariant.BIG => new BigQuiver(),
                null => new SmallQuiver(),
                _ => throw new Exception($"Unknown quiver variant: {QuiverVariant}")
            };

            // Overwrite properties
            q.Name = Name;
            q.Rarity = Rarity;
            q.InventorySpaceAmount = InventorySpaceAmount;
            q.Capacity = Capacity;
            if (QuiverVariant.HasValue)
            {
                q.Variant = QuiverVariant.Value;
            }

            return q;
        }
    }
}