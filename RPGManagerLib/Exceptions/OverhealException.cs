namespace RPGManagerLib.Exceptions
{
    public class OverhealException : CharacterException
    {
        public OverhealException()
            : base("Health cannot exceed 100 points.") { }
    }
}
