using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items.Staffs;
using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Saves;

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
        var sword = new Sword { Rarity = rarity };

        Assert.Equal(expected, sword.GetRarityMultiplier());
    }

    [Fact]
    public void EffectiveStats_ApplyRarityMultiplier()
    {
        var sword = new Sword
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
        var sword = new Sword { Rarity = Rarity.COMMON };

        Assert.Equal(Rarity.UNCOMMON, sword.UpgradeWeapon());
        Assert.Equal(Rarity.RARE, sword.UpgradeWeapon());
        Assert.Equal(Rarity.EPIC, sword.UpgradeWeapon());
        Assert.Equal(Rarity.LEGENDARY, sword.UpgradeWeapon());
        Assert.Equal(Rarity.LEGENDARY, sword.UpgradeWeapon());
    }

    [Fact]
    public void DefaultWeaponConstructors_AssignExpectedTypes()
    {
        Assert.Equal(WeaponType.SWORD, new Sword().Type);
        Assert.Equal(WeaponType.AXE, new Axe().Type);
        Assert.Equal(WeaponType.SPEAR, new Spear().Type);
        Assert.Equal(WeaponType.DAGGER, new Dagger().Type);
        Assert.Equal(WeaponType.STAFF, new Staff().Type);
    }

    [Fact]
    public void Staff_CustomConstructor_SetsProvidedValues()
    {
        var staff = new Staff(80, Rarity.EPIC, 5, "Storm Staff", Element.LIGHTNING);

        Assert.Equal(80, staff.Durability);
        Assert.Equal(Rarity.EPIC, staff.Rarity);
        Assert.Equal(5, staff.Level);
        Assert.Equal("Storm Staff", staff.Name);
        Assert.Equal(Element.LIGHTNING, staff.Element);
    }

    [Fact]
    public void QuiverDefaults_AreCreatedCorrectly()
    {
        var small = new SmallQuiver();
        var medium = new MediumQuiver();

        Assert.Equal(15, small.Capacity);
        Assert.Equal(30, medium.Capacity);
    }

    [Fact]
    public void WeaponSaveData_ToEquipable_ReconstructsWeapon()
    {
        var original = new Sword
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
    }

    [Fact]
    public void QuiverSaveData_ToEquipable_ReconstructsQuiver()
    {
        var original = new SmallQuiver
        {
            Name = "Quick Quiver",
            Rarity = Rarity.RARE,
            Capacity = 55
        };

        var saveData = new QuiverSaveData(original);
        var restored = saveData.ToEquipable();

        Assert.Equal("Quick Quiver", restored.Name);
        Assert.Equal(Rarity.RARE, restored.Rarity);
    }
}
