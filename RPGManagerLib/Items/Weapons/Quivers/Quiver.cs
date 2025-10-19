using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Weapons.Quivers
{
    public abstract class Quiver : IEquipable
    {
        public string Name { get; set; }
        public Rarity Rarity { get; set; }
        public InventorySpaceAmount InventorySpaceAmount { get; set; }
        public int Capacity { get; set; }
        public EquipableType EquipableType => EquipableType.QUIVER;

        protected Quiver(string name, Rarity rarity, InventorySpaceAmount inventorySpaceAmount, int capacity)
        {
            Name = name;
            Rarity = rarity;
            InventorySpaceAmount = inventorySpaceAmount;
            Capacity = capacity;
        }
    }
}