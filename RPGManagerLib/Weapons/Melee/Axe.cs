namespace RPGManagerLib.Weapons.Melee
{
    public class Axe : Weapon
    {

        public Axe()
            : base
                  (damageAmount: 20,
                  durability: 90,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Basic Axe",
                  weaponType: WeaponType.AXE,
                  element: Element.NONE,
                  cooldownTime: 2.5,
                  inventorySpaceAmount: InventorySpaceAmount.LARGE)
        {
        }
        public Axe(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, double coolDownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.AXE, element, coolDownTime, inventorySpaceAmount)
        {

        }
    }
}
