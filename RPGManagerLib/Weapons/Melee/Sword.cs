namespace RPGManagerLib.Weapons.Melee
{
    public class Sword : Weapon
    {
        public Sword()
            : base
                  (damageAmount: 13, 
                  durability: 100, 
                  rarity: Rarity.COMMON, 
                  level: 1, 
                  name: "Basic Sword", 
                  weaponType: WeaponType.SWORD, 
                  element: Element.NONE, 
                  cooldownTime: 1.5, 
                  inventorySpaceAmount: InventorySpaceAmount.SMALL)
        { }

        public Sword(int damageAmount, int durability, Rarity rarity, int level, string name,Element element, double coolDownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.SWORD, element, coolDownTime, inventorySpaceAmount)
        { }
    }
}
