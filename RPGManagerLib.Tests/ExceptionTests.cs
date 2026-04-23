using RPGManagerLib.Exceptions;

namespace RPGManagerLib.Tests;

public class ExceptionTests
{
    [Fact]
    public void CharacterExceptions_HaveExpectedMessages()
    {
        Assert.Equal("Cannot damage negative points.", new NegativeDamageException().Message);
        Assert.Equal("Cannot heal negative points.", new NegativeHealException().Message);
        Assert.Equal("Health cannot exceed 100 points.", new OverhealException().Message);
        Assert.Equal("Damage exceeds the maximum allowed limit (health below -100).", new OverkillException().Message);
        Assert.Equal("'BowX' is not a valid weapon type.", new InvalidWeaponException("BowX").Message);
        Assert.Equal("x", new CharacterException("x").Message);
    }
}
