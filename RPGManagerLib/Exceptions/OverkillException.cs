namespace RPGManagerLib.Exceptions
{
    /// <summary>
    /// Thrown when damage would drop health beyond the permitted lower bound.
    /// </summary>
    public class OverkillException : CharacterException
    {
        public OverkillException()
            : base("Damage exceeds the maximum allowed limit (health below -100).") { }
    }
}

