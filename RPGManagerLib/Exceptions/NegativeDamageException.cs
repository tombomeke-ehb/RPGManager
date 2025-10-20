namespace RPGManagerLib.Exceptions
{
    /// <summary>
    /// Thrown when a negative value is supplied for damage.
    /// </summary>
    public class NegativeDamageException : CharacterException
    {
        public NegativeDamageException()
            : base("Cannot damage negative points.") { }
    }
}

