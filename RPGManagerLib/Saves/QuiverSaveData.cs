using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Weapons.Quivers;
using RPGManagerLib.Items;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Represents the data required to save and restore a quiver.
    /// </summary>
    public class QuiverSaveData : EquipableSaveData
    {
        public int Capacity { get; set; }

        public QuiverSaveData() { }

        public QuiverSaveData(Quiver quiver)
        {
            // Base properties
            Name = quiver.Name;
            Rarity = quiver.Rarity;
            InventorySpaceAmount = quiver.InventorySpaceAmount;

            // Specific properties
            Capacity = quiver.Capacity;
        }

        public override IEquipable ToEquipable()
        {
            // Create instance (currently only supporting SmallQuiver)
            // Add more logic once more quivers get introduced
            Quiver q = new SmallQuiver();

            // Overwrite properties
            q.Name = Name;
            q.Rarity = Rarity;
            q.InventorySpaceAmount = InventorySpaceAmount;
            q.Capacity = Capacity;

            return q;
        }
    }
}