namespace RPGManagerLib.Exceptions
{
    public class RarityUnUpgradableException : Exception
    {
        public RarityUnUpgradableException(string rarity)
            : base($"Can not upgrade higher than {rarity}") { }
    }
}
