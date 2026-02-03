using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items;
using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Saves
{
/// <summary>
/// Serializable snapshot of a character for saving and loading.
/// </summary>
public class CharacterSaveData
{
    /// <summary>
    /// Discriminator describing the concrete character type (e.g., Warrior, Mage).
    /// </summary>
    public string CharacterType { get; set; } = "";

    /// <summary>
    /// Character name.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Current health value.
    /// </summary>
    public double Health { get; set; }

    /// <summary>
    /// Original creation date of the character.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Current power level.
    /// </summary>
    public int PowerLevel { get; set; }

    /// <summary>
    /// Serialized weapons for warriors.
    /// </summary>
    public List<WeaponSaveData> Weapons { get; set; } = new();

    /// <summary>
    /// Mana boost value for mages.
    /// </summary>
    public double Mana { get; set; } // alleen voor Mage

    public int Gold { get; set; }

    public CharacterSaveData() { }

    /// <summary>
    /// Creates save data from a live <see cref="Character"/> instance.
    /// </summary>
    public CharacterSaveData(Character c)
    {
        Name = c.Name;
        Health = c.Health;
        CreationDate = c.CreationDate;
        PowerLevel = c.PowerLevel;
        CharacterType = c.CharacterType;
        Gold = c.Gold;

        if (c is Warrior w)
            Weapons = w.Weapons
                .OfType<Weapon>()
                .Select(x => new WeaponSaveData(x))
                .ToList();
        else if (c is Mage m)
            Mana = m.Mana;
        else if (c is Archer a)
            Weapons = a.Weapons
                .OfType<Weapon>()
                .Select(x => new WeaponSaveData(x))
                .ToList();
    }

    /// <summary>
    /// Reconstructs a live <see cref="Character"/> from this save data.
    /// </summary>
    public Character ToCharacter()
    {
        return CharacterType switch
        {
            "Warrior" => new Warrior(
                Name,
                Health,
                CreationDate,
                PowerLevel,
                Weapons.Select(w => (IEquipable)w.ToWeapon()).ToList(),
                Gold
            ),
            "Archer" => new Archer(
                Name,
                Health,
                CreationDate,
                PowerLevel,
                Weapons.Select(w => (IEquipable)w.ToWeapon()).ToList(),
                Gold
            ),
            "Mage" => new Mage(Name, Health, CreationDate, PowerLevel, Mana),
            _ => throw new Exception($"Unknown character type: {CharacterType}")
        };
    }
}
}
// TODO: Implement Mage Saving and Character Creation