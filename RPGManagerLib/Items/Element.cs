namespace RPGManagerLib.Items
{
    /// <summary>
    /// Elemental damage type shared by weapons and spells. Drives resistance
    /// checks and elemental status effects (e.g. Burn) regardless of whether
    /// the damage came from a melee hit or a cast spell.
    /// </summary>
    public enum Element { NONE, FIRE, ICE, LIGHTNING, POISON, WIND, WATER, EARTH, LIGHT, DARK }
}
