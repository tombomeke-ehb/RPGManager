namespace RPGManagerLib.Exceptions
{
    public class OverkillException : CharacterException
    {
        public OverkillException()
            : base("Damage exceeds the maximum allowed limit (health below -100).") { }
    }
}
