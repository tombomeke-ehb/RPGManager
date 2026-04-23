using System.Text.Json;
using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Saves;

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
        warrior.Weapons.Add(new Sword());

        var save = new CharacterSaveData(warrior);
        var restored = save.ToCharacter() as Warrior;

        Assert.NotNull(restored);
        Assert.Single(restored!.Weapons);
        Assert.Equal("Bjorn", restored.Name);
    }

    [Fact]
    public void CharacterSaveData_Mage_RoundTripsMana()
    {
        var mage = new Mage("Nia") { Mana = 88 };

        var save = new CharacterSaveData(mage);
        var restored = save.ToCharacter() as Mage;

        Assert.NotNull(restored);
        Assert.Equal(88, restored!.Mana);
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
