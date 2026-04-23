using RPGManagerLib.Characters.NPCs;
using RPGManagerLib.Locations;
using RPGManagerLib.Worlds;

namespace RPGManagerLib.Tests;

public class WorldAndLocationTests
{
    [Fact]
    public void Location_Constructors_InitializeValues()
    {
        var l1 = new Location("Town");
        var l2 = new Location("Camp", null!);

        Assert.Equal("Town", l1.Name);
        Assert.Empty(l1.NPCs);
        Assert.Equal("Camp", l2.Name);
        Assert.Empty(l2.NPCs);
    }

    [Fact]
    public void Location_AddAndRemoveNpc_WorkAsExpected()
    {
        var location = new Location("Town");
        var npc = new BlackSmith(location);

        location.AddNPC(npc);
        Assert.Single(location.NPCs);

        location.RemoveNPC(npc);
        Assert.Empty(location.NPCs);
    }

    [Fact]
    public void Location_GetNpcs_WritesNpcNames()
    {
        var location = new Location("Town");
        location.AddNPC(new BlackSmith(location));

        var output = ConsoleTestHelper.CaptureOutput(location.GetNPCs);

        Assert.Contains("Black Smith", output);
    }

    [Fact]
    public void BlackSmith_Methods_WriteExpectedText()
    {
        var location = new Location("Town");
        var npc = new BlackSmith(location);

        var interact = ConsoleTestHelper.CaptureOutput(npc.Interact);
        var trade = ConsoleTestHelper.CaptureOutput(npc.Trade);

        Assert.Contains("blacksmith", interact, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("not ready", trade, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void World_Constructors_SetDefaultsAndProvidedValues()
    {
        var defaultWorld = new World();
        var named = new World("Tavaryn");
        var namedDesc = new World("Tavaryn", "desc");
        var full = new World("X", "Y", 9, true);

        Assert.Equal("Unknown World", defaultWorld.Name);
        Assert.False(defaultWorld.IsUnlocked);

        Assert.Equal("Tavaryn", named.Name);
        Assert.Equal("A world waiting to be discovered.", named.Description);

        Assert.Equal("desc", namedDesc.Description);
        Assert.Equal(9, full.DifficultyLevel);
        Assert.True(full.IsUnlocked);
    }

    [Fact]
    public void World_AddRemoveAndLockUnlock_WorkAsExpected()
    {
        var world = new World("Tavaryn");
        var location = new Location("Town");

        world.AddLocation(location);
        world.AddLocation(location);

        Assert.Single(world.Locations);

        world.RemoveLocation(location);
        Assert.Empty(world.Locations);

        world.Unlock();
        Assert.True(world.IsUnlocked);

        world.Lock();
        Assert.False(world.IsUnlocked);
    }
}
