using RPGManagerLib.Items;

namespace RPGManagerLib.Characters.Heroes
{
    public class Archer : Character
    {
        public List<IEquipable> Weapons { get; set; } = new();

        public override string CharacterType => "Archer";

        public Archer() : base() { }

        public Archer(string name) : base(name) { }

        /// <summary>
        /// Returns the archer's equipped weapons as a comma-separated list with rarity.
        /// </summary>
        protected override string GetEquipmentLine()
        {
            // Convert the weapon list into one display string for the character sheet.
            return Weapons != null && Weapons.Any()
                ? string.Join(", ", Weapons.Select(w => $"{w.Name} ({w.Rarity})"))
                : "none";
        }
    }
}
