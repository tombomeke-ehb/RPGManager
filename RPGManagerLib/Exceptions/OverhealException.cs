namespace RPGManagerLib.Exceptions
{
    /// <summary>
    /// Thrown when healing would exceed the maximum allowed health.
    /// </summary>
    public class OverhealException : CharacterException
    {
        public OverhealException()
            : base("Health cannot exceed 100 points.") { }
    }
}

