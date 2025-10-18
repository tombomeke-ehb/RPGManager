namespace RPGManagerLib.Weapons
{
    public abstract class Weapon
    {
        public int DamageAmount { get; set; }
        public int Durability { get; set; }
        public Rarity Rarity { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public WeaponType Type { get; set; }
        public Element Element { get; set; } = Element.NONE;
        public double CooldownTime { get; set; }
        public InventorySpaceAmount InventorySpaceAmount { get; set; }

        public Weapon(int damageAmount, int durability, Rarity rarity, int level, string name, WeaponType weaponType, Element element,double cooldownTime, InventorySpaceAmount inventorySpaceAmount)
        {
            DamageAmount = damageAmount;
            Durability = durability;
            Rarity = rarity;
            Level = level;
            Name = name;
            Type = weaponType;
            Element = element;
            CooldownTime = cooldownTime;
            InventorySpaceAmount = inventorySpaceAmount;
        }

        public double GetRarityMultiplier() =>
            Rarity switch
            {
                Rarity.COMMON => 1.0,
                Rarity.UNCOMMON => 1.2,
                Rarity.RARE => 1.5,
                Rarity.EPIC => 2.0,
                Rarity.LEGENDARY => 3.0,
                _ => 1.0
            };

        public int GetEffectiveDamage() => (int)(DamageAmount * GetRarityMultiplier());
        public int GetEffectiveDurability() => (int)(Durability * GetRarityMultiplier());

        public Rarity UpgradeWeapon() { 
            Rarity = Rarity switch
            {
                Rarity.COMMON => Rarity.UNCOMMON,
                Rarity.UNCOMMON => Rarity.RARE,
                Rarity.RARE => Rarity.EPIC,
                Rarity.EPIC => Rarity.LEGENDARY,
                _ => throw new NotImplementedException()
            };
            return Rarity;
        }
    }

    public enum Rarity { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }
    public enum InventorySpaceAmount { SMALL, LARGE }
    public enum WeaponType { SWORD, STAFF, AXE, SPEAR, DAGGER, SIMPLEBOW }
    public enum Element { NONE, FIRE, ICE, LIGHTNING, POISON }
}