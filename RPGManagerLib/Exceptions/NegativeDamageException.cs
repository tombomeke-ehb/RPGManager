namespace RPGManagerLib.Exceptions
{
    public class NegativeDamageException : CharacterException
    {
        public NegativeDamageException()
            : base("Cannot damage negative points.") { }
    }
}
