namespace RPGManagerLib.Items
{
    /// <summary>
    /// Defines the categories of items that can be equipped.
    /// </summary>
    public enum EquipableType
    {
        /// <summary>
        /// A weapon such as a sword, axe, spear, or bow.
        /// </summary>
        WEAPON,

        /// <summary>
        /// A quiver used to carry arrows for bows.
        /// </summary>
        QUIVER,

        /// <summary>
        /// A staff used by mages to channel magical energy. Typically has lower physical damage but may have elemental or magical effects.
        /// </summary>
        STAFF
        //TODO Later add: ARMOR, RING, etc.
    }
}
