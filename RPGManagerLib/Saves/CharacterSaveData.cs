using RPGManagerLib.Characters;

public class CharacterSaveData
{
    public string CharacterType { get; set; } = "";
    public string Name { get; set; } = "";
    public double Health { get; set; }
    public DateTime CreationDate { get; set; }
    public int PowerLevel { get; set; }
    public List<WeaponSaveData> Weapons { get; set; } = new();
    public double ManaBoost { get; set; } // alleen voor Mage

    public CharacterSaveData() { }

    public CharacterSaveData(Character c)
    {
        Name = c.Name;
        Health = c.Health;
        CreationDate = c.CreationDate;
        PowerLevel = c.PowerLevel;
        CharacterType = c.CharacterType;

        if (c is Warrior w)
            Weapons = w.Weapons.Select(x => new WeaponSaveData(x)).ToList();
        else if (c is Mage m)
            ManaBoost = m.ManaBoost;
    }

    public Character ToCharacter()
    {
        return CharacterType switch
        {
            "Warrior" => new Warrior(Name, Health, CreationDate, PowerLevel, Weapons.Select(w => w.ToWeapon()).ToList()),
            "Mage" => new Mage(Name, Health, CreationDate, PowerLevel, ManaBoost),
            _ => throw new Exception($"Unknown character type: {CharacterType}")
        };
    }
}
