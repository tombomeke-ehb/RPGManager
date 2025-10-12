namespace RPGManagerLib.Exceptions
{
    public class NegativeHealException : CharacterException
    {
        public NegativeHealException()
            : base("Cannot heal negative points.") { }
    }
}
