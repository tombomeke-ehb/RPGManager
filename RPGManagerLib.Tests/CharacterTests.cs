using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Items.Staffs;
using RPGManagerLib.Worlds;

namespace RPGManagerLib.Tests;

public class CharacterTests
{
    [Fact]
    public void DefaultConstructor_SetsExpectedDefaults()
    {
        var warrior = new Warrior();

        Assert.Equal("John", warrior.Name);
        Assert.Equal(100, warrior.Health);
        Assert.Equal(1, warrior.PowerLevel);
        Assert.Equal(50, warrior.Gold);
        Assert.Equal(100, warrior.Mana);
    }

    [Fact]
    public void NamedConstructor_SetsName()
    {
        var warrior = new Warrior("Conan");

        Assert.Equal("Conan", warrior.Name);
    }

    [Fact]
    public void Heal_WithValidPoints_IncreasesHealth()
    {
        var warrior = new Warrior();
        warrior.Damage(20);

        warrior.Heal(10);

        Assert.Equal(90, warrior.Health);
    }

    [Fact]
    public void Heal_WithNegativePoints_DoesNotChangeHealth()
    {
        var warrior = new Warrior();
        var output = ConsoleTestHelper.CaptureOutput(() => warrior.Heal(-1));

        Assert.Equal(100, warrior.Health);
        Assert.Contains("Heal failed", output);
    }

    [Fact]
    public void Heal_WithOverheal_DoesNotChangeHealth()
    {
        var warrior = new Warrior();
        warrior.Damage(1);

        var output = ConsoleTestHelper.CaptureOutput(() => warrior.Heal(2));

        Assert.Equal(99, warrior.Health);
        Assert.Contains("Heal failed", output);
    }

    [Fact]
    public void Damage_WithValidPoints_DecreasesHealth()
    {
        var warrior = new Warrior();

        warrior.Damage(25);

        Assert.Equal(75, warrior.Health);
    }

    [Fact]
    public void Damage_WithNegativePoints_DoesNotChangeHealth()
    {
        var warrior = new Warrior();
        var output = ConsoleTestHelper.CaptureOutput(() => warrior.Damage(-1));

        Assert.Equal(100, warrior.Health);
        Assert.Contains("Damage failed", output);
    }

    [Fact]
    public void Damage_WithOverkill_DoesNotChangeHealth()
    {
        var warrior = new Warrior();
        var output = ConsoleTestHelper.CaptureOutput(() => warrior.Damage(250));

        Assert.Equal(100, warrior.Health);
        Assert.Contains("Damage failed", output);
    }

    [Fact]
    public void Damage_WhenHealthReachesZero_WritesDeathMessage()
    {
        var warrior = new Warrior();

        var output = ConsoleTestHelper.CaptureOutput(() => warrior.Damage(100));

        Assert.Equal(0, warrior.Health);
        Assert.Contains("Character has died.", output);
    }

    [Fact]
    public void TravelTo_UnlockedWorld_SetsCurrentWorld()
    {
        var warrior = new Warrior();
        var world = new World("Tavaryn") { IsUnlocked = true };

        warrior.TravelTo(world);

        Assert.Same(world, warrior.CurrentWorld);
    }

    [Fact]
    public void TravelTo_LockedWorld_DoesNotSetCurrentWorld()
    {
        var warrior = new Warrior();
        var world = new World("Locked") { IsUnlocked = false };

        warrior.TravelTo(world);

        Assert.Null(warrior.CurrentWorld);
    }

    [Fact]
    public void ToString_IncludesEquipmentLine()
    {
        var warrior = new Warrior("Hero");
        warrior.Weapons.Add(new Sword());

        var text = warrior.ToString();

        Assert.Contains("Hero the Warrior", text);
        Assert.Contains("Basic Sword", text);
    }

    [Fact]
    public void ToString_ForArcherAndMage_UsesClassSpecificEquipmentLine()
    {
        var archer = new Archer("Arrow");
        archer.Weapons.Add(new Sword());

        var mage = new Mage("Arc");
        mage.Weapons.Add(new Staff());

        var archerText = archer.ToString();
        var mageText = mage.ToString();

        Assert.Contains("Arrow the Archer", archerText);
        Assert.Contains("Basic Sword", archerText);
        Assert.Contains("Arc the Mage", mageText);
        Assert.Contains("Basic Staff", mageText);
    }
}
