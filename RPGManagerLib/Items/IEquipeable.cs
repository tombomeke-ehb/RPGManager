using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items
{
    public interface IEquipable
    {
        string Name { get; }
        Rarity Rarity { get; }
        InventorySpaceAmount InventorySpaceAmount { get; }
        EquipableType EquipableType { get; }
    }
}
