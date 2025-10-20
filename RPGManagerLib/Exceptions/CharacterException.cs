namespace RPGManagerLib.Exceptions
{
    /// <summary>
    /// Base exception type for character-related validation issues.
    /// </summary>
    public class CharacterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterException"/> class with a message.
        /// </summary>
        public CharacterException(string message) : base(message)
        {
        }
    }
}

