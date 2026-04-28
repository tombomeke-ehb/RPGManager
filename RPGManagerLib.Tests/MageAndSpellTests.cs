using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Staffs;
using RPGManagerLib.Spells;

namespace RPGManagerLib.Tests;

public class MageAndSpellTests
{
    [Fact]
    public void Mage_Constructors_SetManaToMaxMana()
    {
        var mage1 = new Mage();
        var mage2 = new Mage("Merlin");

        Assert.Equal(150, mage1.Mana);
        Assert.Equal(150, mage2.Mana);
        Assert.Equal(150, mage2.MaxMana);
    }

    [Fact]
    public void CastSpell_WithInvalidIndex_WritesMessage()
    {
        var mage = new Mage();
        var target = new Warrior();

        var output = ConsoleTestHelper.CaptureOutput(() => mage.CastSpell(0, target));

        Assert.Contains("Invalid spell.", output);
    }

    [Fact]
    public void SpellCast_WithInsufficientMana_DoesNotDamageTarget()
    {
        var mage = new Mage("Ezra") { Mana = 10 };
        mage.Spells.Add(new Fireball());
        var target = new Warrior();

        var output = ConsoleTestHelper.CaptureOutput(() => mage.CastSpell(0, target));

        Assert.Equal(100, target.Health);
        Assert.Equal(10, mage.Mana);
        Assert.Contains("Not enough mana!", output);
    }

    [Fact]
    public void SpellCast_UsesStaffElementMultiplier_WhenElementMatchesSpell()
    {
        var mage = new Mage("Ezra") { Mana = 100 };
        mage.Spells.Add(new Fireball());
        mage.Weapons.Add(new Staff(100, Rarity.COMMON, 1, "Flame Staff", Element.FIRE));
        var target = new Warrior();

        mage.CastSpell(0, target);

        Assert.Equal(80, mage.Mana);
        Assert.Equal(62.5, target.Health);
    }

    [Fact]
    public void FireballAndIceSpike_HaveExpectedDefaults()
    {
        var fireball = new Fireball();
        var iceSpike = new IceSpike();

        Assert.Equal("Fireball", fireball.Name);
        Assert.Equal(30, fireball.BaseDamage);
        Assert.Equal(20, fireball.ManaCost);

        Assert.Equal("Ice Spike", iceSpike.Name);
        Assert.Equal(25, iceSpike.BaseDamage);
        Assert.Equal(15, iceSpike.ManaCost);
    }
}
