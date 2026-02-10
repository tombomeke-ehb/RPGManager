using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Represents the data required to save and restore any IEquipable item.
    /// </summary>
    public class EquipableSaveData
    {
        /// <summary>
        /// Discriminator to determine the concrete type (Weapon or Quiver).
        /// </summary>
        public EquipableType EquipableType { get; set; }

        /// <summary>
        /// Weapon-specific save data (null if not a weapon).
        /// </summary>
        public WeaponSaveData? WeaponData { get; set; }

        /// <summary>
        /// Quiver-specific save data (null if not a quiver).
        /// </summary>
        public QuiverSaveData? QuiverData { get; set; }

        public EquipableSaveData() { }

        /// <summary>
        /// Creates save data from any IEquipable item.
        /// </summary>
        public EquipableSaveData(IEquipable equipable)
        {
            EquipableType = equipable.EquipableType;

            if (equipable is Weapon weapon)
            {
                WeaponData = new WeaponSaveData(weapon);
            }
            else if (equipable is Quiver quiver)
            {
                QuiverData = new QuiverSaveData(quiver);
            }
        }

        /// <summary>
        /// Converts this save data back to an IEquipable instance.
        /// </summary>
        public IEquipable ToEquipable()
        {
            return EquipableType switch
            {
                EquipableType.WEAPON => WeaponData?.ToWeapon() 
                    ?? throw new Exception("WeaponData is null for WEAPON type"),
                EquipableType.QUIVER => QuiverData?.ToQuiver() 
                    ?? throw new Exception("QuiverData is null for QUIVER type"),
                _ => throw new Exception($"Unknown equipable type: {EquipableType}")
            };
        }
    }
}
