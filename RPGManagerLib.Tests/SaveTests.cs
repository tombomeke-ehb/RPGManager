using System.Text.Json;
using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items.Staffs;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Melee.Swords;
using RPGManagerLib.Items.Weapons.Quivers;
using RPGManagerLib.Saves;
using RPGManagerLib.UI;

namespace RPGManagerLib.Tests;

public class SaveTests : IDisposable
{
    private readonly string _saveFolder = Path.Combine(AppContext.BaseDirectory, "Save");
    private readonly string _saveFile;

    public SaveTests()
    {
        _saveFile = Path.Combine(_saveFolder, "characters.json");
        if (Directory.Exists(_saveFolder))
        {
            Directory.Delete(_saveFolder, true);
        }
    }

    [Fact]
    public void CharacterSaveData_Warrior_RoundTripsWeapons()
    {
        var warrior = new Warrior("Bjorn");
        warrior.Weapons.Add(new GreatSword());
        warrior.Weapons.Add(new SmallQuiver());

        var save = new CharacterSaveData(warrior);
        var restored = save.ToCharacter() as Warrior;

        Assert.NotNull(restored);
        Assert.Equal(2, restored!.Weapons.Count);
        Assert.Equal("Bjorn", restored.Name);
        Assert.Contains(restored.Weapons, item => item is GreatSword);
        Assert.Contains(restored.Weapons, item => item is SmallQuiver);
    }

    [Fact]
    public void CharacterSaveData_Mage_RoundTripsManaAndWeapons()
    {
        var mage = new Mage("Nia") { Mana = 88 };
        mage.Weapons.AddRange(CharacterFactory.CreateDefaultWeaponsMage());

        var save = new CharacterSaveData(mage);
        var restored = save.ToCharacter() as Mage;

        Assert.NotNull(restored);
        Assert.Equal(88, restored!.Mana);
        Assert.Contains(restored.Weapons, item => item.Name == "Basic Staff");
        Assert.Contains(restored.Weapons, item => item.Name == "Basic Dagger");
    }

    [Fact]
    public void CharacterSaveData_Archer_RoundTripsBowAndQuiver()
    {
        var archer = new Archer("Vale");
        archer.Weapons.AddRange(CharacterFactory.CreateDefaultWeaponsArcher());

        var save = new CharacterSaveData(archer);
        var restored = save.ToCharacter() as Archer;

        Assert.NotNull(restored);
        Assert.Equal("Vale", restored!.Name);
        Assert.Contains(restored.Weapons, item => item is Weapon weapon && weapon.Type == WeaponType.BOW);
        Assert.Contains(restored.Weapons, item => item is SmallQuiver);
    }

    [Fact]
    public void CharacterSaveData_ToCharacter_UnknownType_Throws()
    {
        var save = new CharacterSaveData { CharacterType = "Unknown" };

        Assert.Throws<Exception>(() => save.ToCharacter());
    }

    [Fact]
    public void SaveManager_LoadCharacters_WhenMissingFile_ReturnsEmptyList()
    {
        var loaded = SaveManager.LoadCharacters();

        Assert.Empty(loaded);
    }

    [Fact]
    public void SaveManager_SaveAndLoadCharacters_RoundTrip()
    {
        var characters = new List<Character>
        {
            new Warrior("Ares"),
            new Archer("Robin"),
            new Mage("Vex")
        };

        SaveManager.SaveCharacters(characters);
        var loaded = SaveManager.LoadCharacters();

        Assert.Equal(3, loaded.Count);
        Assert.Contains(loaded, c => c.Name == "Ares" && c is Warrior);
        Assert.Contains(loaded, c => c.Name == "Robin" && c is Archer);
        Assert.Contains(loaded, c => c.Name == "Vex" && c is Mage);
    }

    [Fact]
    public void SaveManager_LoadCharacters_WithInvalidJson_ReturnsEmptyList()
    {
        Directory.CreateDirectory(_saveFolder);
        File.WriteAllText(_saveFile, "{ invalid json }");

        var loaded = SaveManager.LoadCharacters();

        Assert.Empty(loaded);
    }

    public void Dispose()
    {
        if (Directory.Exists(_saveFolder))
        {
            Directory.Delete(_saveFolder, true);
        }
    }
}
