using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;
using System.Text.Json.Serialization;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Base class for all saveable equipable items.
    /// Uses polymorphic JSON serialization to handle different item types cleanly.
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "ItemType")]
    [JsonDerivedType(typeof(WeaponSaveData), typeDiscriminator: "Weapon")]
    [JsonDerivedType(typeof(QuiverSaveData), typeDiscriminator: "Quiver")]
    public abstract class EquipableSaveData
    {
        public string Name { get; set; } = "";
        public Rarity Rarity { get; set; }
        public InventorySpaceAmount InventorySpaceAmount { get; set; }

        public abstract IEquipable ToEquipable();
    }
}