using RPGManagerLib.Locations;

namespace RPGManagerLib.Characters.NPCs
{
    public class BlackSmith : NPC
    {
        public BlackSmith(Location location)
            : base("Black Smith", location)
        {
        }

        public override void Interact()
        {
            throw new NotImplementedException();
        }

        public override void Trade()
        {
            throw new NotImplementedException();
        }
    }
}
