using System.Reflection;
using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.UI;

namespace RPGManagerLib.Tests;

public class UiTests
{
    [Fact]
    public void CharacterFactory_DefaultWeaponLists_ReturnExpectedCounts()
    {
        Assert.Single(CharacterFactory.CreateDefaultWeaponsWarrior());
        Assert.Equal(3, CharacterFactory.CreateDefaultWeaponsArcher().Count);
        Assert.Equal(2, CharacterFactory.CreateDefaultWeaponsMage().Count);
    }

    [Fact]
    public void CharacterFactory_PrivateCreateMage_CreatesMageWithDefaultLoadout()
    {
        var mage = InvokeCharacterFactoryCreate<Mage>("CreateMage", "Mina");

        Assert.Equal("Mina", mage.Name);
        Assert.Equal(2, mage.Weapons.Count);
        Assert.Equal(2, mage.Spells.Count);
    }

    [Fact]
    public void CharacterFactory_PrivateCreateArcher_CreatesArcherWithDefaultLoadout()
    {
        var archer = InvokeCharacterFactoryCreate<Archer>("CreateArcher", "Rook");

        Assert.Equal("Rook", archer.Name);
        Assert.Equal(3, archer.Weapons.Count);
    }

    [Fact]
    public void CharacterFactory_PrivateCreateWarrior_WithSimulatedInput_AddsEquipment()
    {
        Warrior warrior = null!;
        ConsoleTestHelper.CaptureOutput(
            () => warrior = InvokeCharacterFactoryCreate<Warrior>("CreateWarrior", "Brak"),
            "1 3\n");

        Assert.Equal("Brak", warrior.Name);
        Assert.Equal(2, warrior.Weapons.Count);
    }

    [Fact]
    public void MenuSystem_AddOption_StoresActionsByKey()
    {
        var menu = new MenuSystem("TEST");
        menu.AddOption("1", "Do thing", () => { });
        menu.AddOption("1", "Override", () => { });

        var field = typeof(MenuSystem).GetField("options", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var options = (System.Collections.IDictionary)field.GetValue(menu)!;

        Assert.Single(options);
    }

    [Fact]
    public void GameMenu_PrivateExploreAndFight_WhenNoCharacter_WriteSelectionMessage()
    {
        SetGameMenuState(new List<Character>(), null);

        var exploreOutput = ConsoleTestHelper.CaptureOutput(() => InvokeGameMenuMethod("Explore"));
        var fightOutput = ConsoleTestHelper.CaptureOutput(() => InvokeGameMenuMethod("Fight"));

        Assert.Contains("Select a character first", exploreOutput);
        Assert.Contains("Select a character first", fightOutput);
    }

    private static void SetGameMenuState(List<Character> characters, Character? current)
    {
        var type = typeof(GameMenu);
        type.GetField("characters", BindingFlags.NonPublic | BindingFlags.Static)!.SetValue(null, characters);
        type.GetField("currentCharacter", BindingFlags.NonPublic | BindingFlags.Static)!.SetValue(null, current);
    }

    private static void InvokeGameMenuMethod(string methodName)
    {
        var type = typeof(GameMenu);
        type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static)!.Invoke(null, null);
    }

    private static TCharacter InvokeCharacterFactoryCreate<TCharacter>(string methodName, string name)
        where TCharacter : Character
    {
        var method = typeof(CharacterFactory).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static)!;
        return (TCharacter)method.Invoke(null, new object[] { name })!;
    }
}
