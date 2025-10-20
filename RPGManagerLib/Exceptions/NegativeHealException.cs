namespace RPGManagerLib.Exceptions
{
    /// <summary>
    /// Thrown when a negative value is supplied for healing.
    /// </summary>
    public class NegativeHealException : CharacterException
    {
        public NegativeHealException()
            : base("Cannot heal negative points.") { }
    }
}

