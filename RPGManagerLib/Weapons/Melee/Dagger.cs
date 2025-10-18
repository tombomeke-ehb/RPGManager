namespace RPGManagerLib.Weapons.Melee
{
    public class Dagger : Weapon
    {
        public Dagger()
            : base(damageAmount: 8,
                  durability: 50,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Simple Dagger",
                  weaponType: WeaponType.DAGGER,
                  element: Element.NONE,
                  cooldownTime: 1,
                  inventorySpaceAmount: InventorySpaceAmount.SMALL

            )
        { }

        public Dagger(int damageAmount, int durability, Rarity rarity, int level, string name, WeaponType weaponType, Element element, double cooldownTime, InventorySpaceAmount inventorySpaceAmount)
            : base(damageAmount, durability, rarity, level, name, weaponType, element, cooldownTime, inventorySpaceAmount)
        {
        }
    }
}
