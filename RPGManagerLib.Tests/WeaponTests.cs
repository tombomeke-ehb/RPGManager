using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Staffs;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items.Weapons.Melee.Axes;
using RPGManagerLib.Items.Weapons.Melee.Daggers;
using RPGManagerLib.Items.Weapons.Melee.Swords;
using RPGManagerLib.Items.Weapons.Melee.Spears;
using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Saves;
using RPGManagerLib.Weapons.Quivers;

namespace RPGManagerLib.Tests;

public class WeaponTests
{
    [Theory]
    [InlineData(Rarity.COMMON, 1.0)]
    [InlineData(Rarity.UNCOMMON, 1.2)]
    [InlineData(Rarity.RARE, 1.5)]
    [InlineData(Rarity.EPIC, 2.0)]
    [InlineData(Rarity.LEGENDARY, 3.0)]
    public void GetRarityMultiplier_ReturnsExpectedValue(Rarity rarity, double expected)
    {
        var sword = new TestSword { Rarity = rarity };

        Assert.Equal(expected, sword.GetRarityMultiplier());
    }

    [Fact]
    public void EffectiveStats_ApplyRarityMultiplier()
    {
        var sword = new TestSword
        {
            DamageAmount = 10,
            Durability = 20,
            Rarity = Rarity.RARE
        };

        Assert.Equal(15, sword.GetEffectiveDamage());
        Assert.Equal(30, sword.GetEffectiveDurability());
    }

    [Fact]
    public void UpgradeWeapon_ProgressesUntilLegendary()
    {
        var sword = new TestSword { Rarity = Rarity.COMMON };

        Assert.Equal(Rarity.UNCOMMON, sword.UpgradeWeapon());
        Assert.Equal(Rarity.RARE, sword.UpgradeWeapon());
        Assert.Equal(Rarity.EPIC, sword.UpgradeWeapon());
        Assert.Equal(Rarity.LEGENDARY, sword.UpgradeWeapon());
        Assert.Equal(Rarity.LEGENDARY, sword.UpgradeWeapon());
    }

    [Fact]
    public void WeaponFamilies_AssignExpectedTypes()
    {
        Assert.Equal(WeaponType.SWORD, new TestSword().Type);
        Assert.Equal(WeaponType.AXE, new TestAxe().Type);
        Assert.Equal(WeaponType.SPEAR, new TestSpear().Type);
        Assert.Equal(WeaponType.DAGGER, new TestDagger().Type);
        Assert.Equal(WeaponType.STAFF, new TestStaff().Type);
        Assert.Equal(WeaponType.BOW, new TestBow().Type);
    }

    [Fact]
    public void WeaponFamilies_AssignExpectedVariantValues()
    {
        Assert.Equal(SwordVariant.BASIC, new TestSword().Variant);
        Assert.Equal(AxeVariant.BASIC, new TestAxe().Variant);
        Assert.Equal(DaggerVariant.BASIC, new TestDagger().Variant);
        Assert.Equal(SpearVariant.BASIC, new TestSpear().Variant);
        Assert.Equal(StaffVariant.BASIC, new TestStaff().Variant);
        Assert.Equal(BowVariant.SIMPLE, new TestBow().Variant);
    }

    [Fact]
    public void QuiverDefaults_AreCreatedCorrectly()
    {
        var small = new SmallQuiver();
        var medium = new MediumQuiver();
        var large = new BigQuiver();

        Assert.Equal(15, small.Capacity);
        Assert.Equal(30, medium.Capacity);
        Assert.Equal(50, large.Capacity);
        Assert.Equal(QuiverVariant.SMALL, small.Variant);
        Assert.Equal(QuiverVariant.MEDIUM, medium.Variant);
        Assert.Equal(QuiverVariant.BIG, large.Variant);
    }

    [Fact]
    public void WeaponSaveData_ToEquipable_ReconstructsWeapon()
    {
        var original = new GreatSword
        {
            Name = "Test Sword",
            Rarity = Rarity.EPIC,
            DamageAmount = 42,
            Durability = 99,
            Level = 6,
            Element = Element.FIRE
        };

        var saveData = new WeaponSaveData(original);
        var restored = saveData.ToEquipable() as Weapon;

        Assert.NotNull(restored);
        Assert.Equal("Test Sword", restored!.Name);
        Assert.Equal(Rarity.EPIC, restored.Rarity);
        Assert.Equal(42, restored.DamageAmount);
        Assert.Equal(99, restored.Durability);
        Assert.Equal(6, restored.Level);
        Assert.Equal(Element.FIRE, restored.Element);
        Assert.Equal(WeaponType.SWORD, restored.Type);
        Assert.Equal(nameof(GreatSword), restored.GetType().Name);
    }

    [Fact]
    public void WeaponSaveData_ToEquipable_ReconstructsBowVariant()
    {
        var saveData = new WeaponSaveData
        {
            Name = "Siege Bow",
            Rarity = Rarity.RARE,
            InventorySpaceAmount = InventorySpaceAmount.LARGE,
            WeaponType = WeaponType.BOW,
            BowVariant = BowVariant.WAR,
            DamageAmount = 51,
            Durability = 65,
            Level = 4,
            Element = Element.POISON
        };

        var restored = saveData.ToEquipable() as Weapon;

        Assert.NotNull(restored);
        Assert.Equal("Siege Bow", restored!.Name);
        Assert.Equal(Rarity.RARE, restored.Rarity);
        Assert.Equal(51, restored.DamageAmount);
        Assert.Equal(65, restored.Durability);
        Assert.Equal(4, restored.Level);
        Assert.Equal(Element.POISON, restored.Element);
        Assert.Equal(WeaponType.BOW, restored.Type);
        Assert.Equal(InventorySpaceAmount.LARGE, restored.InventorySpaceAmount);
        Assert.Equal("WarBow", restored.GetType().Name);
    }

    [Fact]
    public void QuiverSaveData_ToEquipable_ReconstructsQuiver()
    {
        var original = new SmallQuiver
        {
            Name = "Quick Quiver",
            Rarity = Rarity.RARE,
            Capacity = 55,
            InventorySpaceAmount = InventorySpaceAmount.LARGE
        };

        var saveData = new QuiverSaveData(original);
        var restored = saveData.ToEquipable() as Quiver;

        Assert.NotNull(restored);
        Assert.IsType<SmallQuiver>(restored);
        Assert.Equal("Quick Quiver", restored!.Name);
        Assert.Equal(Rarity.RARE, restored.Rarity);
        Assert.Equal(55, restored.Capacity);
        Assert.Equal(InventorySpaceAmount.LARGE, restored.InventorySpaceAmount);
    }

    private sealed class TestSword : Sword
    {
        public TestSword()
            : base(10, 100, Rarity.COMMON, 1, "Test Sword", Element.NONE, SwordVariant.BASIC, InventorySpaceAmount.SMALL)
        {
        }
    }

    private sealed class TestAxe : Axe
    {
        public TestAxe()
            : base(10, 100, Rarity.COMMON, 1, "Test Axe", Element.NONE, AxeVariant.BASIC, InventorySpaceAmount.LARGE)
        {
        }
    }

    private sealed class TestSpear : Spear
    {
        public TestSpear()
            : base(10, 100, Rarity.COMMON, 1, "Test Spear", Element.NONE, SpearVariant.BASIC, InventorySpaceAmount.SMALL)
        {
        }
    }

    private sealed class TestDagger : Dagger
    {
        public TestDagger()
            : base(10, 100, Rarity.COMMON, 1, "Test Dagger", Element.NONE, DaggerVariant.BASIC, InventorySpaceAmount.SMALL)
        {
        }
    }

    private sealed class TestStaff : Staff
    {
        public TestStaff()
            : base(3, 100, Rarity.COMMON, 1, "Test Staff", Element.NONE, StaffVariant.BASIC)
        {
        }
    }

    private sealed class TestBow : RPGManagerLib.Items.Weapons.Bows.Bow
    {
        public TestBow()
            : base(10, 100, Rarity.COMMON, 1, "Test Bow", BowVariant.SIMPLE, Element.NONE, InventorySpaceAmount.LARGE)
        {
        }
    }
}
