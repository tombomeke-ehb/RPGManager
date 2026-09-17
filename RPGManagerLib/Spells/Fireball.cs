using RPGManagerLib.Items;

namespace RPGManagerLib.Spells
{
    public class Fireball : Spell
    {
        public Fireball()
            : base("Fireball", Element.FIRE, 30, 20)
        { }
    }

    public class IceSpike : Spell
    {
        public IceSpike()
            : base("Ice Spike", Element.ICE, 25, 15)
        { }
    }

}
