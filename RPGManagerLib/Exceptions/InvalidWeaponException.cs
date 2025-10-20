namespace RPGManagerLib.Exceptions
{
    /// <summary>
    /// Thrown when an input or selection does not correspond to any known weapon type.
    /// </summary>
    public class InvalidWeaponException : Exception
    {
        /// <summary>
        /// Creates an exception that includes the offending weapon name.
        /// </summary>
        public InvalidWeaponException(string weaponName)
            : base($"'{weaponName}' is not a valid weapon type.")
        {
        }
    }
}
