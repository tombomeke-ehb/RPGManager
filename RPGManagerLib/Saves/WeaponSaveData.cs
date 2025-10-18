using RPGManagerLib.Weapons;
using RPGManagerLib.Weapons.Bows;
using RPGManagerLib.Weapons.Melee;
public class WeaponSaveData
{
    public WeaponType WeaponType { get; set; }
    public string Name { get; set; } = "";
    public int DamageAmount { get; set; }
    public int Durability { get; set; }
    public Rarity Rarity { get; set; }
    public int Level { get; set; }
    public Element Element { get; set; }
    public double CoolDownTime { get; set; }
    public InventorySpaceAmount InventorySpaceAmount { get; set; }

    public WeaponSaveData() { }

    public WeaponSaveData(Weapon weapon)
    {
        WeaponType = weapon.Type;
        Name = weapon.Name;
        DamageAmount = weapon.DamageAmount;
        Durability = weapon.Durability;
        Rarity = weapon.Rarity;
        Level = weapon.Level;
        Element = weapon.Element;
        CoolDownTime = weapon.CooldownTime;
        InventorySpaceAmount = weapon.InventorySpaceAmount;
    }

    public Weapon ToWeapon()
    {
        return WeaponType switch
        {
            WeaponType.SWORD => new Sword(
                DamageAmount,
                Durability,
                Rarity,
                Level,
                Name,
                Element,
                CoolDownTime,
                InventorySpaceAmount
            ),
            WeaponType.AXE => new Axe(
                DamageAmount,
                Durability,
                Rarity,
                Level,
                Name,
                Element,
                CoolDownTime,
                InventorySpaceAmount
            ),
            WeaponType.SPEAR => new Spear(
                DamageAmount,
                Durability,
                Rarity,
                Level,
                Name,
                Element,
                CoolDownTime,
                InventorySpaceAmount
            ),
            WeaponType.SIMPLEBOW => new SimpleBow(
                DamageAmount,
                Durability,
                Rarity,
                Level,
                Name,
                Element,
                CoolDownTime,
                InventorySpaceAmount
            ),
            _ => throw new Exception($"Unknown weapon type: {WeaponType}")
        };
    }
}
