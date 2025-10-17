namespace RPGManagerLib.Weapons
{
    public class Axe : Weapon
    {

        public Axe()
            : base(20,
                  90,
                  Rarity.COMMON,
                  1,
                  "Basic Axe",
                  WeaponType.AXE,
                  Element.NONE,
                  2.5,
                  InventorySpaceAmount.LARGE)
        {
        }
        public Axe(int damageAmount, int durability, Rarity rarity, int level, string name, Element element, double coolDownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, WeaponType.AXE, element, coolDownTime, inventorySpaceAmount)
        {

        }
    }
}
