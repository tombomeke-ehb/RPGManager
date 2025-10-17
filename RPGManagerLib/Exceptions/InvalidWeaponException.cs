namespace RPGManagerLib.Weapons
{
    public class InvalidWeaponException : Exception
    {
        public InvalidWeaponException(string weaponName)
            : base($"'{weaponName}' is not a valid weapon type.")
        {
        }
    }
}
