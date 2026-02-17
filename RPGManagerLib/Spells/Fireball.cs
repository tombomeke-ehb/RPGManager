using RPGManagerLib.Items;

namespace RPGManagerLib.Spells
{
    public class Fireball : Spell
    {
        public Fireball()
            : base("Fireball", MagicType.FIRE, 30, 20)
        { }
    }

    public class IceSpike : Spell
    {
        public IceSpike()
            : base("Ice Spike", MagicType.ICE, 25, 15)
        { }
    }

}
